using System;
using System.Collections.Generic;
using System.IO;

namespace Enrollment_TextFile
{
    class Program
    {
        // 객체를 담아둘 리스트 생성 
        public static List<Subject> subjectList = new List<Subject>();

        // 한 줄씩 분리한 텍스트를 받아서 객체로 만들고 리스트에 넣는 메쏘드 
        public static void DisassembleSubjects(string subject)
        {
            string[] a = subject.Split('/');
            Subject oneSubject = new Subject(a);
            subjectList.Add(oneSubject);
        }

        // 메인 화면
        public static void PrintMainScreen()
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("                   2021 수강신청");
            Console.WriteLine("-----------------------------------뒤로가기:ESC---");
            Console.WriteLine();
            Console.WriteLine("1. 수강신청");
            Console.WriteLine("2. 관심과목");
            Console.WriteLine("3. 나의 시간표");
            Console.WriteLine("4. 종료");
            Console.WriteLine();
            Console.Write("원하시는 메뉴의 숫자를 입력해주세요: ");

            bool isInputWrong = true;

            while (isInputWrong)
            {
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":

                        isInputWrong = false;
                        break;
                    case "2":

                        isInputWrong = false;
                        break;
                    case "3":

                        isInputWrong = false;
                        break;
                    case "4":
                        // 창을 꺼야 함 
                        isInputWrong = false;
                        break;
                    default:
                        // set cursor 필요 
                        Console.Write("잘못된 값을 입력했습니다. 다시 입력하세요: ");
                        isInputWrong = true;
                    break;
                }
            }
            
        }

        static void Main(string[] args)
        {
            try
            {
                string text = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "timetable.txt");
                //Console.WriteLine("{0}", text);
                string[] subjects = text.Split('#');

                // 텍스트 파일에서 가져온 텍스트를 객체에 집어넣고 객체 리스트에 넣기 
                for (int i=1; i<21; i++)
                {
                    DisassembleSubjects(subjects[i]);
                    Console.WriteLine(subjectList[i - 1].name);
                }

            }
            catch (SystemException e)
            {
                Console.WriteLine(e.Message);
            }

            PrintMainScreen();
            
        }
    }
}


