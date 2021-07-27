using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect_SDA
{
    public partial class Form1 : Form
    {
        PictureBox[] tiles=new PictureBox[9] ;
        State initial = new State(3);
        HashTable visited=new HashTable();
        State solution;
        Point[] pozTilesCorecte = new Point[9];
        public class DuplicateKeyComparer<TKey>
                :
             IComparer<TKey> where TKey : IComparable
        {

            public int Compare(TKey x, TKey y)
            {
                int result = x.CompareTo(y);

                if (result == 0)
                    return 1;  
                else
                    return result;
            }

        }
        public Form1()
        {
            InitializeComponent();
            tiles[0] = pictureBox1;
            tiles[1] = pictureBox2;
            tiles[2] = pictureBox3;
            tiles[3] = pictureBox4;
            tiles[4] = pictureBox5;
            tiles[5] = pictureBox6;
            tiles[6] = pictureBox7;
            tiles[7] = pictureBox8;
            tiles[8] = pictureBox9;
            for (int i = 0; i < 9; i++)
                pozTilesCorecte[i] = tiles[i].Location;
            varInit.Text = "7 2 4\r\n5 0 6\r\n8 3 1\r\n";

            varFinala.Clear();
        }
         
        public SortedList<int,State> expand(State toExpand)
        {
            SortedList<int, State> final = new SortedList<int, State>(new DuplicateKeyComparer<int>());
            State temp = new State(toExpand.dim);

            temp.copy( toExpand);
            if (temp.space.Item1 - 1 >= 0)
			{
                temp.MoveSpaceTo(temp.space.Item1 - 1, temp.space.Item2);

				if (!visited.checkElem(temp.HashCode(),temp.mat))
				{
                    temp.moves_made++;
                    visited.Add(temp.HashCode(), temp.mat);
                    final.Add(temp.Manhattan_distance(),temp);

				}
			}
            temp = new State(toExpand.dim);
            temp.copy(toExpand);
            if (temp.space.Item1 + 1 <= toExpand.dim-1)
            {
                temp.MoveSpaceTo(temp.space.Item1 + 1, temp.space.Item2);

                if (!visited.checkElem(temp.HashCode(), temp.mat))
                {
                    temp.moves_made++;
                    visited.Add(temp.HashCode(), temp.mat);
                    final.Add(temp.Manhattan_distance(), temp);
                }
            }
            temp = new State(toExpand.dim);
            temp.copy(toExpand);
            if (temp.space.Item2 - 1 >= 0)
            {
                temp.MoveSpaceTo(temp.space.Item1, temp.space.Item2 - 1);

                if (!visited.checkElem(temp.HashCode(), temp.mat))
                {
                    temp.moves_made++;
                    visited.Add(temp.HashCode(), temp.mat);
                    final.Add(temp.Manhattan_distance(), temp);
                }
            }
            temp = new State(toExpand.dim);
            temp.copy(toExpand);
            if (temp.space.Item2 + 1 <= toExpand.dim-1)
            {
                temp.MoveSpaceTo(temp.space.Item1, temp.space.Item2 + 1);

                if (!visited.checkElem(temp.HashCode(), temp.mat))
                {
                    temp.moves_made++;
                    visited.Add(temp.HashCode(), temp.mat);
                    final.Add(temp.Manhattan_distance(), temp);
                }
            }
            //foreach(KeyValuePair<int,State> x in final)
            //{
            //    for (int i = 0; i <x.Value.dim; i++)
            //    {
            //        for (int j = 0; j < x.Value.dim; j++)
            //        {
            //            varFinala.AppendText(x.Value.mat[i, j]+" ");
            //        }
            //        varFinala.AppendText("\r\n");
            //    }
            //    varFinala.AppendText("\r\n____________________________________\r\n ");
            //}
            //varFinala.AppendText("\r\n___________gata___________________\r\n ");
            return final;
        }
        public State solve(State initial)
        {
            varFinala.Clear();
            State solution = new State(initial.dim),aux=new State(initial.dim);
            SortedList<int, State> toVisit = new SortedList<int, State>(new DuplicateKeyComparer<int>()),temp = new SortedList<int, State>(new DuplicateKeyComparer<int>());
            solution.SolutionInit();

            visited.Add(initial.HashCode(), initial.mat);
            toVisit.Add(initial.Manhattan_distance(), initial);

            while(!HashTable.equalMatrix(aux.mat,solution.mat))
            {
                aux = new State(initial.dim);
                aux.copy(toVisit.First().Value);
                toVisit.RemoveAt(0);
                temp = expand(aux);
                foreach(KeyValuePair<int,State> x in temp)
                {
                    toVisit.Add(x.Key,x.Value);
                }
                temp.Clear();
            }
            visited.clear();
            return aux;
        }
        
        public void MoveUpAnimated()
        {
            if(initial.space.Item1 - 1<0)
            {
                MessageBox.Show("Nu pot muta!");
                return;
            }
            tiles[8].Location = pozTilesCorecte[(initial.space.Item1 - 1) * 3 + initial.space.Item2];
            tiles[initial.mat[initial.space.Item1 - 1, initial.space.Item2]-1].Location = pozTilesCorecte[initial.space.Item1 * 3 + initial.space.Item2];
            initial.MoveSpaceTo(initial.space.Item1 - 1, initial.space.Item2);
        }
        public void MoveDownAnimated()
        {
            if (initial.space.Item1 + 1 >=3)
            {
                MessageBox.Show("Nu pot muta!");
                return;
            }
            tiles[8].Location = pozTilesCorecte[(initial.space.Item1 + 1) * 3 + initial.space.Item2];
            tiles[initial.mat[initial.space.Item1+1,initial.space.Item2]-1].Location = pozTilesCorecte[initial.space.Item1 * 3 + initial.space.Item2];
            initial.MoveSpaceTo(initial.space.Item1 + 1, initial.space.Item2);
        }
        public void MoveRightAnimated()
        {
            if (initial.space.Item2 + 1 >= 3)
            {
                MessageBox.Show("Nu pot muta!");
                return;
            }
            tiles[8].Location = pozTilesCorecte[initial.space.Item1 * 3 + initial.space.Item2 + 1];
            tiles[initial.mat[initial.space.Item1, initial.space.Item2 + 1] - 1].Location = pozTilesCorecte[initial.space.Item1 * 3 + initial.space.Item2];
            initial.MoveSpaceTo(initial.space.Item1, initial.space.Item2 + 1);
        }
        public void MoveLeftAnimated()
        {
            if (initial.space.Item2 - 1 < 0)
            {
                MessageBox.Show("Nu pot muta!");
                return;
            }
            tiles[8].Location = pozTilesCorecte[initial.space.Item1 * 3 + initial.space.Item2 - 1];
            tiles[initial.mat[initial.space.Item1, initial.space.Item2 - 1] - 1].Location = pozTilesCorecte[initial.space.Item1 * 3 + initial.space.Item2];
            initial.MoveSpaceTo(initial.space.Item1, initial.space.Item2 - 1);
        }
        public int posTile(int x)
        {
            int rez = 0;
            for(int i=0; i<9;i++)
            {

            }
            return rez;
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            State solution,temp=new State(3);
            temp.SolutionInit();
            SortedList<int, State> caleInapoi=new SortedList<int, State>();
            if (HashTable.equalMatrix(initial.mat, temp.mat))
            {
                MessageBox.Show("Puzzle neinitializat!");
                return;
            }
            if (initial.space==null)
            {
                MessageBox.Show("Puzzle neinitializat!");
                return;
            }
            if(!initial.Validate())
            {
                MessageBox.Show("Puzzle imposibil de rezolvat!");
                return;
            }
            solution=solve(initial);
            while (solution.made_move!=MoveSpace.ROOT)
            {
                caleInapoi.Add(solution.moves_made, solution);
                solution = solution.parent;
            }
            caleInapoi.Add(0, initial);
            foreach(KeyValuePair<int,State> x in caleInapoi)
            {
                varFinala.AppendText(x.Value.made_move.ToString() + "\r\n");
                for(int i=0;i<x.Value.dim;i++)
                {
                    for(int j=0;j<x.Value.dim;j++)
                        varFinala.AppendText(x.Value.mat[i, j]+" ");

                    varFinala.AppendText("\r\n");
                }
                varFinala.AppendText("\r\n");
                switch(x.Value.made_move)
                {
                    case MoveSpace.UP:
                        {
                            MoveUpAnimated();
                        }; break;
                    case MoveSpace.DOWN:
                        {
                            MoveDownAnimated();
                        }; break;
                    case MoveSpace.RIGHT:
                        {
                            MoveRightAnimated();
                        }; break;
                    case MoveSpace.LEFT:
                        {
                            MoveLeftAnimated();
                        }; break;
                    case MoveSpace.ROOT:
                        {

                        }; break;
                }
                this.Refresh();
                System.Threading.Thread.Sleep(500);
            }
            varFinala.AppendText(caleInapoi[caleInapoi.Count-1].moves_made.ToString());
        }
        private void button2_Click(object sender, EventArgs e)
        {
            bool possible = false;
            State temp = new State(3);

            while(!possible)
            {

                int[] sir = new int[9];
                for(int i=0;i<9;i++)
                {
                    Random rand=new Random();
                    int x=0;
                    bool ok = true;
                    while(ok)
                    {
                        x= rand.Next(0, 9);
                        ok = false;
                        for (int j = 0; j < i && !ok; j++)
                            if (x == sir[j])
                                ok = true;
                    }
                    sir[i] = x;
                    if (sir[i] == 0)
                        tiles[8].Location = pozTilesCorecte[i];
                    else
                        tiles[sir[i] - 1].Location = pozTilesCorecte[i];
                }
                temp.initMat(sir);
                if (temp.Validate())
                    possible = true;
            }
            initial.copy(temp);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            State temp = new State(varInit.Text, 3);
            initial.copy(temp);
            for(int i=0;i<3;i++)
            {
                for(int j=0;j<3;j++)
                {
                    if(initial.mat[i, j]==0)
                        tiles[8].Location = pozTilesCorecte[i * 3 + j];
                    else
                        tiles[initial.mat[i, j]-1].Location = pozTilesCorecte[i*3+j];
                }
            }
        }
    }
}
