using UnityEngine;
using System.Collections;

public class FluidSim : MonoBehaviour {
	const float ADD_TIME = 5f;

	public float dt = 0.8f;
	public float visc = 0;
	public float volatilize = 0;
	public int iterations = 10;
	public Texture2D border;
	public Texture2D flow;
	public bool curdled = false;

	Texture2D tex;
	int width, height;
	float[] u, v, u_prev, v_prev;
	float[] dens, dens_prev;
	float[] bndX, bndY;
	float[] velX, velY;
	int rowSize;
	int size;

	float t, radX, radY = 0;
	float timeForAdds = ADD_TIME;
	bool add = false;

	void Start() {
		// basic empty texture
		tex = new Texture2D(32, 32,  TextureFormat.Alpha8, false);
		tex.filterMode = FilterMode.Bilinear;
		GetComponent<Renderer>().material.mainTexture = tex;
		width = tex.width;
		height = tex.height;

		// initialize fluid arrays
		rowSize = width + 2;
		size = (width+2)*(height+2);
		dens = new float[size];
		dens_prev = new float[size];
		u = new float[size];
		u_prev = new float[size];
		v = new float[size];
		v_prev = new float[size];
		bndX = new float[size];
		bndY = new float[size];
		velX = new float[size];
		velY = new float[size];
		for (int i = 0; i < size; i++) {
			dens_prev[i] = u_prev[i] = v_prev[i] = dens[i] = u[i] = v[i] = 0;
			int y = i / rowSize;
			int x = i % rowSize;
			bndX[i] = border.GetPixel(x, y).grayscale - border.GetPixel(x+1, y).grayscale;
			bndY[i] = border.GetPixel(x, y).grayscale - border.GetPixel(x, y+1).grayscale;
			velX[i] = (flow.GetPixel(x, y).grayscale - flow.GetPixel(x+1, y).grayscale) * 0.1f;
			velY[i] = (flow.GetPixel(x, y).grayscale - flow.GetPixel(x, y+1).grayscale) * 0.1f;
		}
	}

	void Update() {
		// reset values
		for (int i = 0; i < size; i++) {
			dens_prev[i] = 0;
			u_prev[i] = velX[i];
			v_prev[i] = velY[i];
		}
		AddMilk();
		vel_step(u, v, u_prev, v_prev, dt);
		dens_step(dens, dens_prev, u, v, dt);
		Draw();
	}
	
	void addFields(float[] x, float[] s, float dt) {
		for (int i=0; i<size ; i++ ) {
			x[i] += dt*s[i];
		}
	}

	void set_bnd(int b, float[] x) {
		// b/w texture as obstacles
		for (int j = 1; j <= height; j++) {
			for (int i = 1; i <= width; i++) {
				int p = i + j * width;
				if (bndX[p] < 0) {
					x[p] = (b == 1) ? -x[p + 1] : x[p + 1];
				}
				if (bndX[p] > 0) {
					x[p] = (b == 1) ? -x[p - 1] : x[p - 1];
				}
				if (bndY[p] < 0) {
					x[p] = (b == 2) ? -x[p + rowSize] : x[p + rowSize];
				}
				if (bndY[p] > 0) {
					x[p] = (b == 2) ? -x[p - rowSize] : x[p - rowSize];
				}
			}
		}
	}
	
	void lin_solve(float[] x, float[] x0, float a, float c) {
		if (a == 0 && c == 1) {
			for (int i = 0; i < size; i++) {
				x[i] = x0[i];
			}
			set_bnd(0, x);
		} else {
			for (int k=0 ; k<iterations; k++) {
				for (int j=1 ; j<=height; j++) {
					int lastRow = (j - 1) * rowSize;
					int currentRow = j * rowSize;
					int nextRow = (j + 1) * rowSize;
					float lastX = x[currentRow];
					++currentRow;
					for (int i=1; i<=width; i++) {
						lastX = x[currentRow] = (x0[currentRow] + a * (lastX + x[++currentRow] + x[++lastRow] + x[++nextRow])) / c;
					}
				}
				set_bnd(0, x);
			}
		}
	}
	
	void diffuse(float[] x, float[] x0){
		lin_solve(x, x0, volatilize, 1 + 4 * volatilize);
	}
	
	void lin_solve2(float[] x, float[] x0, float[] y, float[] y0, float a, float c) {
		if (a == 0 && c == 1) {
			for (int i = 0; i < size; i++) {
				x[i] = x0[i];
				y[i] = y0[i];
			}
			set_bnd(1, x);
			set_bnd(2, y);
		} else {
			for (int k=0 ; k<iterations; k++) {
				for (int j=1 ; j <= height; j++) {
					int lastRow = (j - 1) * rowSize;
					int currentRow = j * rowSize;
					int nextRow = (j + 1) * rowSize;
					float lastX = x[currentRow];
					float lastY = y[currentRow];
					++currentRow;
					for (int i=1; i<=width; i++) {
						lastX = x[currentRow] = (x0[currentRow] + a * (lastX + x[currentRow] + x[lastRow] + x[nextRow])) / c;
						lastY = y[currentRow] = (y0[currentRow] + a * (lastY + y[++currentRow] + y[++lastRow] + y[++nextRow])) / c;
					}
				}
				set_bnd(1, x);
				set_bnd(2, y);
			}
		}
	}
	
	void diffuse2(float[] x, float[] x0, float[] y, float[] y0) {
		lin_solve2(x, x0, y, y0, visc, 1 + 4 * visc);
	}
	
	void advect(int b, float[] d, float[] d0, float[] u, float[] v, float dt) {
		float dt0 = dt * width;
		float Wp5 = width + 0.5f;
		float Hp5 = height + 0.5f;
		for (int j = 1; j<= height; j++) {
			int pos = j * rowSize;
			for (int i = 1; i <= width; i++) {
				float x = i - dt0 * u[++pos]; 
				float y = j - dt0 * v[pos];
				if (x < 0.5f) {
					x = 0.5f;
				} else if (x > Wp5) {
					x = Wp5;
				}
				int i0 = (int)x;
				int i1 = i0 + 1;
				if (y < 0.5f) {
					y = 0.5f;
				} else if (y > Hp5) {
					y = Hp5;
				}
				int j0 = (int)y;
				int j1 = j0 + 1;
				float s1 = x - i0;
				float s0 = 1 - s1;
				float t1 = y - j0;
				float t0 = 1 - t1;
				int row1 = j0 * rowSize;
				int row2 = j1 * rowSize;
				d[pos] = s0 * (t0 * d0[i0 + row1] + t1 * d0[i0 + row2]) + s1 * (t0 * d0[i1 + row1] + t1 * d0[i1 + row2]);
			}
		}
		set_bnd(b, d);
	}
	
	void project(float[] u, float[] v, float[] p, float[] div) {
		float h = -0.5f / Mathf.Sqrt(width * height);
		for (int j = 1; j <= height; j++ ) {
			int row = j * rowSize;
			int previousRow = (j - 1) * rowSize;
			int prevValue = row - 1;
			int currentRow = row;
			int nextValue = row + 1;
			int nextRow = (j + 1) * rowSize;
			for (int i = 1; i <= width; i++ ) {
				div[++currentRow] = h * (u[++nextValue] - u[++prevValue] + v[++nextRow] - v[++previousRow]);
				p[currentRow] = 0;
			}
		}
		set_bnd(0, div);
		set_bnd(0, p);
		
		lin_solve(p, div, 1, 4);

		float scale = 0.5f * width;
		for (int j = 1; j<= height; j++ ) {
			int prevPos = j * rowSize - 1;
			int currentPos = j * rowSize;
			int nextPos = j * rowSize + 1;
			int prevRow = (j - 1) * rowSize;
			int nextRow = (j + 1) * rowSize;
			for (int i = 1; i<= width; i++) {
				u[++currentPos] -= scale * (p[++nextPos] - p[++prevPos]);
				v[currentPos]   -= scale * (p[++nextRow] - p[++prevRow]);
			}
		}
		set_bnd(1, u);
		set_bnd(2, v);
	}
	
	void dens_step(float[] x, float[] x0, float[] u, float[] v, float dt) {
		addFields(x, x0, dt);
		diffuse(x0, x);
		advect(0, x, x0, u, v, dt );
	}
	
	void vel_step(float[] u, float[] v, float[] u0, float[] v0, float dt) {
		float[] temp;
		addFields(u, u0, dt);
		addFields(v, v0, dt);
		temp = u0; u0 = u; u = temp;
		temp = v0; v0 = v; v = temp;
		diffuse2(u, u0, v, v0);
		project(u, v, u0, v0);
		temp = u0; u0 = u; u = temp; 
		temp = v0; v0 = v; v = temp;
		advect(1, u, u0, u0, v0, dt);
		advect(2, v, v0, u0, v0, dt);
		project(u, v, u0, v0);
	}

	public void StartAdd() {
		add = true;
		timeForAdds = ADD_TIME;
	}

	void AddMilk() {
		if(!add){ return; }
		timeForAdds -= Time.deltaTime;

		int x, y;

		if(curdled){
			x = Random.Range(0, width);
			y = Random.Range(0, height);
		} else {
			// outward spiral parametric equation
			t += Time.deltaTime;
			radX += Time.deltaTime;
			radY += Time.deltaTime;

			if(t > (2*Mathf.PI)) { t = 0; }
			if(radX > width/2) { radX = 0; }
			if(radY > height/2) { radY = 0; }

			x = (int) (width/2 + Mathf.Cos(t)*radX);
			y = (int) (height/2 + Mathf.Sin(t)*radY);
		}

		int i = (x + 1) + (y + 1) * rowSize;
		dens_prev[i] += 3f;
		u_prev[i] += 0.025f;
		v_prev[i] += 0.025f;

		if(timeForAdds <= 0){ add = false; }
	}
	
	void Draw()
	{
		// visualize water
		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				int i = (x + 1) + (y + 1) * rowSize;
				float d = 5f * dens[i] + u[i] + v[i];

				if(d > 0.7f) { d = 0.7f; }

				tex.SetPixel(x, y, new Color(1,1,1, d));
			}
		}
		tex.Apply(false);
	}
}
