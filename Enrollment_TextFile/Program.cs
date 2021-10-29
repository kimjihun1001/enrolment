using System;
using System.Collections.Generic;
using System.IO;

namespace Enrollment_TextFile
{
    class Program
    {
        // 메인 메쏘드 
        static void Main(string[] args)
        {
            try
            {
                string text = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "timetable.txt");
                //Console.WriteLine("{0}", text);
                string[] subjectTextList = text.Split('\n');

                // 텍스트 파일에서 가져온 텍스트를 객체에 집어넣고 객체 리스트에 넣기 
                for (int i=1; i<21; i++)
                {
                    PrintScreen.DisassembleSubjects(subjectTextList[i]);
                    //Console.WriteLine(subjectList[i - 1].name);
                }
            }
            catch (SystemException e)
            {
                Console.WriteLine(e.Message);
            }

            PrintScreen.PrintMainScreen();
        }
    }
}


