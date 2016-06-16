using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplitAuthors
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathInput = @"C:\Users\cweb\Desktop\input.csv";
            string pathOutputJSON = @"C:\Users\cweb\Desktop\input.csv";
            string pathOutputCSV = @"C:\Users\cweb\Desktop\input.csv";


            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("ano");
            dt.Columns.Add("titulo");
            dt.Columns.Add("autores");
            dt.Columns.Add("autor1");
            dt.Columns.Add("autor2");
            dt.Columns.Add("autor3");
            dt.Columns.Add("autor4");
            dt.Columns.Add("autor5");
            dt.Columns.Add("autor6");
            dt.Columns.Add("autor7");
            dt.Columns.Add("autor8");
            dt.Columns.Add("autor9");
            dt.Columns.Add("autor10");
            dt.Columns.Add("autor11");


            StreamReader arq = new StreamReader(pathInput);

            while (!arq.EndOfStream)
            {
                string[] linha = arq.ReadLine().Split(';');

                DataRow row = dt.NewRow();
                row["id"] = linha[0];
                row["autores"] = linha[1];
                row["titulo"] = linha[2];
                row["ano"] = linha[3];

                string[] stringSeparators = new string[] { " and " };
                string[] authors = row["autores"].ToString().Split(stringSeparators, StringSplitOptions.None);

                for (int i = 0; i < authors.Length && i < 10; i++)
                {
                    row["autor" + (i + 1)] = authors[i].ToString();
                }

                dt.Rows.Add(row);


                arq.Read();
            }

            arq.Close();

            #region gera JSON
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(dt);

           ;
            TextWriter tw = new StreamWriter(pathOutputJSON, false);
            tw.WriteLine(json);
            tw.Close();
            #endregion

            #region gera um novo CSV
            StreamWriter newCSV = new StreamWriter(pathOutputCSV, true);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string line = "";
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    line += dt.Rows[i][c].ToString() + ";";
                }
                newCSV.WriteLine(line);

            }
            newCSV.Close();
            #endregion



        }
    }
}
