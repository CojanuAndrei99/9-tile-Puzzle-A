using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect_SDA
{
	public enum MoveSpace
	{
		ROOT,
		RIGHT,
		LEFT,
		UP,
		DOWN
	}
	public class State
	{
		public	int[,] mat;
		public int dim, moves_made;
		public Tuple<int, int> space;
		public State parent;
		public MoveSpace made_move;

		public State(int d)
		{
			dim = d;
			parent =null;
			mat = new int[dim,dim];
			moves_made = 0;
			made_move = MoveSpace.ROOT;
			space = null;
		}

		public State(string text,int d)
		{
			string[] lines = text.Split('\n');
			dim = d;
			parent = null;
			moves_made = 0;
			made_move = MoveSpace.ROOT;
			mat = new int[dim, dim];
			for(int i=0;i<dim;i++)
			{
				string[] nr = lines[i].Split(' ');
				for(int j=0;j<dim;j++)
				{
					mat[i, j] = int.Parse(nr[j]);
					if(mat[i,j]==0)
					{
						space = Tuple.Create(i, j);
					}
				}
			}
			
		}

		public void initMat(int[] sir)
		{
			for(int i=0;i<Math.Sqrt(sir.Length);i++)
				for (int j = 0; j < Math.Sqrt(sir.Length); j++)
				{
					mat[i, j] = sir[i * (int)Math.Sqrt(sir.Length) + j];
					if (sir[i * (int)Math.Sqrt(sir.Length) + j] == 0)
						space = new Tuple<int, int>(i, j);
				}
		}
		public int Manhattan_distance()
		{
			int dist = 0, i_final, j_final, dist_i, dist_j;
			for (int i = 0; i < dim; i++)
			{
				for (int j = 0; j < dim; j++)
				{
					if (mat[i,j] != 0)
					{
						i_final = (mat[i,j] - 1) / dim;
						j_final = (mat[i,j] - 1) % dim;
						dist_i = i - i_final;
						dist_j = j - j_final;
						dist = dist + Math.Abs(dist_i) + Math.Abs(dist_j);
					}
				}
			}
			return dist+moves_made;
		}
		public void copy(State source)
		{
			dim = source.dim;
			moves_made = source.moves_made;
			space = source.space;
			parent = source.parent;
			made_move = source.made_move;
			for(int i=0;i<dim;i++)
			{
				for(int j=0;j<dim; j++)
				{
					mat[i,j] = source.mat[i, j];
				}
			}
		}
		public int NumarInversiuni()
		{
			int contor = 0, inv;
			int[] vect = new int[dim * dim];

			for (int i = 0; i < dim; i++)
			{
				for (int j = 0; j < dim; j++)
				{
					vect[(i * dim) + j] = mat[i,j];
				}
			}
			for (int i = 0; i < dim * dim - 1; i++)
			{
				if (vect[i] != 0)
				{
					inv = 0;
					for (int j = i + 1; j < dim * dim; j++)
					{
						if (vect[i] > vect[j] && vect[j] != 0)
						{
							inv++;
						}
					}
					contor += inv;
				}
			}
			return contor;
		}

		public void MoveSpaceTo(int i,int j)
		{
			int aux;
			State temp=new State(this.dim);
			temp.copy(this);
			if (i < space.Item1)
				made_move = MoveSpace.UP;
			if (i > space.Item1)
				made_move = MoveSpace.DOWN;
			if (j < space.Item2)
				made_move = MoveSpace.LEFT;
			if (j > space.Item2)
				made_move = MoveSpace.RIGHT;
			aux = mat[i, j];
			mat[i, j] = mat[space.Item1, space.Item2];
			mat[space.Item1, space.Item2] = aux;
			space = Tuple.Create(i, j);
			this.parent = temp;
		}

		public void SolutionInit()
		{
			for (int i = 0; i < dim; i++)
			{
				for (int j = 0; j < dim; j++)
				{
					mat[i,j] = i * dim + j + 1;
				}
			}
			mat[dim - 1,dim - 1] = 0;
		}

		public bool Validate()
		{
			int inv = NumarInversiuni();

			if (dim % 2 == 1)
			{
				if (inv % 2 == 0)
					return true;
				else
					return false;
			}
			else
			{
				if ((dim - space.Item1) % 2 == 0)
				{
					if (inv % 2 != 0)
						return true;
					else
						return false;
				}
				else
				{
					if (inv % 2 == 0)
						return true;
					else
						return false;
				}
			}
		}

		public Int64 HashCode()
		{
			Int64 rez = 0;
			for (int i = 0; i < dim; i++)
			{
				for (int j = 0; j < dim; j++)
				{
					rez = rez * 10 + mat[i,j];
				}
			}
			return rez%Int64.MaxValue;
		}

	} 
}
