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
        public SchoolRepository<Test> RepTest { get; } = new SchoolRepository<Test>();
        public SchoolRepository<Mark> RepMark { get; } = new SchoolRepository<Mark>();

        public event ProgressHandler Progress;

        public void ImportAsync(string filename)
        {
            new Thread(() => { Import(new FileStream(filename, FileMode.Open)); }).Start();
        }

        public void Import(Stream stream)
        {
            RepPupil.DeleteAll();
            RepForm.DeleteAll();
            StreamReader sr = new StreamReader(stream, Encoding.Default);
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
                    this.Progress(bytesRead * 100 / (int)stream.Length);
            }
        }

        public void PersistMark(int testid, int pupilid, int mark)
        {
            
        }
    }
}
