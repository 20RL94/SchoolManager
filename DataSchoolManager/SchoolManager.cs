using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataSchoolManager
{
    public delegate void ProgressHandler(int percent);

    public class SchoolManager
    {
        public SchoolRepository<Form> RepForm { get; } = new SchoolRepository<Form>();
        public SchoolRepository<Pupil> RepPupil { get; } = new SchoolRepository<Pupil>();

        public event ProgressHandler Progress;

        public void ImportAsync(string filename)
        {
            new Thread(() => { Import(filename); }).Start();
        }

        public void Import(string filename)
        {
            filename = @"C:\Users\MET\Dropbox\School\PR.CSharp\part3\Uebungen\05_PupilFinder\ListeAllerSchuelerLeonding.csv";
            RepPupil.DeleteAll();
            RepForm.DeleteAll();
            StreamReader sr = new StreamReader((string)filename, Encoding.Default);
            FileInfo fi = new FileInfo((string)filename);
            int bytesRead = sr.ReadLine().Length + 2;
            while (!sr.EndOfStream)
            {
                string str = sr.ReadLine();
                bytesRead += str.Length + 2;
                string[] data = str.Split(';');
                string formname = data[0];
                Form form = RepForm.Get(f => f.Name == formname).FirstOrDefault();
                if (form == null)
                {
                    form = new Form { Name = formname };
                    RepForm.Create(form);
                }
                RepPupil.Create(new Pupil
                {
                    Firstname = data[3],
                    Lastname = data[2],
                    MatrikelNo = data[1],
                    Sex = data[4].ToLower(),
                    Birthday = DateTime.Parse(data[5]),
                    FormId = form.FormId
                });
                // Methodenaufruf über den Delegate
                if (this.Progress != null)
                    this.Progress(bytesRead * 100 / (int)fi.Length);
            }
        }
    }
}
