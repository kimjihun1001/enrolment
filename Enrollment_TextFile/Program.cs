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

        // 수강신청한 목록을 담아둘 리스트 생성 
        public static List<Subject> enrolmentList = new List<Subject>();

        // 수강신청된 과목의 총 학점 계산 함수
        public static int SumTotalUnit()
        {
            int unitTotal = 0;
            for (int i=0; i<enrolmentList.Count; i++)
            {
                unitTotal += int.Parse(enrolmentList[i].unit);
            }
            return unitTotal;
        }

        // 총 학점
        public static int unitTotal = SumTotalUnit();

        // 콘솔 클리어 & 타이틀
        public static void PrintTitle()
        {
            Console.Clear();
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("██████╗  ██████╗ ██████╗  ██╗    ███████╗███╗   ██╗██████╗  ██████╗ ██╗     ███╗   ███╗███████╗███╗   ██╗████████╗");
            Console.WriteLine("╚════██╗██╔═████╗╚════██╗███║    ██╔════╝████╗  ██║██╔══██╗██╔═══██╗██║     ████╗ ████║██╔════╝████╗  ██║╚══██╔══╝");
            Console.WriteLine(" █████╔╝██║██╔██║ █████╔╝╚██║    █████╗  ██╔██╗ ██║██████╔╝██║   ██║██║     ██╔████╔██║█████╗  ██╔██╗ ██║   ██║   ");
            Console.WriteLine("██╔═══╝ ████╔╝██║██╔═══╝  ██║    ██╔══╝  ██║╚██╗██║██╔══██╗██║   ██║██║     ██║╚██╔╝██║██╔══╝  ██║╚██╗██║   ██║   ");
            Console.WriteLine("███████╗╚██████╔╝███████╗ ██║    ███████╗██║ ╚████║██║  ██║╚██████╔╝███████╗██║ ╚═╝ ██║███████╗██║ ╚████║   ██║   ");
            Console.WriteLine("╚══════╝ ╚═════╝ ╚══════╝ ╚═╝    ╚══════╝╚═╝  ╚═══╝╚═╝  ╚═╝ ╚═════╝ ╚══════╝╚═╝     ╚═╝╚══════╝╚═╝  ╚═══╝   ╚═╝   ");
            Console.WriteLine("-----------------------------------------------------------------------------------------------뒤로가기:ESC--------");
            Console.WriteLine();
        }

        // 메인 화면 
        public static void PrintMainScreen()
        {
            PrintTitle();
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
                        PrintScreen_1();
                        isInputWrong = false;
                        break;
                    case "2":
                        PrintScreen_2();
                        isInputWrong = false;
                        break;
                    case "3":
                        PrintScreen_3();
                        isInputWrong = false;
                        break;
                    case "4":
                        // 창을 꺼야 함
                        Environment.Exit(0);
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

        // 1번 화면: 수강 신청 
        public static void PrintScreen_1()
        {
            PrintTitle();
            Console.WriteLine("1. 수강 강의 추가");
            Console.WriteLine("2. 수강 강의 삭제");
            Console.WriteLine("3. 수강 강의 조회");
            Console.WriteLine("4. 전체 강의 목록");
            Console.WriteLine("5. 강의 검색");
            Console.WriteLine("6. 처음 화면으로 돌아가기");
            Console.WriteLine("7. 종료");
            Console.WriteLine();
            Console.Write("원하시는 메뉴의 숫자를 입력해주세요: ");

            bool isInputWrong = true;

            while (isInputWrong)
            {
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        PrintScreen_1_1();
                        isInputWrong = false;
                        break;
                    case "2":
                        PrintScreen_1_2();
                        isInputWrong = false;
                        break;
                    case "3":
                        PrintScreen_1_3();
                        isInputWrong = false;
                        break;
                    case "4":
                        PrintScreen_1_4();
                        isInputWrong = false;
                        break;
                    case "5":
                        PrintScreen_1_5();
                        isInputWrong = false;
                        break;
                    case "6":
                        PrintMainScreen();
                        isInputWrong = false;
                        break;
                    case "7":
                        Environment.Exit(0);
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

        // 1-1 화면: 수강 강의 추가 
        public static void PrintScreen_1_1()
        {
            Console.Clear();
            Console.WriteLine("1. 개설 학과 전공으로 검색하여 수강 신청");
            Console.WriteLine("2. 학수 번호로 검색하여 수강 신청");
            Console.WriteLine("3. 교과목 명으로 검색하여 수강 신청");
            Console.WriteLine("4. 강의 대상 학년으로 검색하여 수강 신청");
            Console.WriteLine("5. 교수명으로 검색하여 수강 신청");
            Console.WriteLine("6. 관심과목으로 검색하여 수강 신청");
            Console.WriteLine("7. 처음 화면으로 돌아가기");
            Console.WriteLine("8. 종료");
            Console.WriteLine();
            Console.Write("원하시는 메뉴의 숫자를 입력해주세요: ");

            bool isInputWrong = true;

            while (isInputWrong)
            {
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        PrintScreen_1_1_1();
                        isInputWrong = false;
                        break;
                    case "2":
                        PrintScreen_1_1_2();
                        isInputWrong = false;
                        break;
                    case "3":
                        PrintScreen_1_1_3();
                        isInputWrong = false;
                        break;
                    case "4":
                        PrintScreen_1_1_4();
                        isInputWrong = false;
                        break;
                    case "5":
                        PrintScreen_1_1_5();
                        isInputWrong = false;
                        break;
                    case "6":
                        PrintScreen_1_1_6();
                        isInputWrong = false;
                        break;
                    case "7":
                        PrintMainScreen();
                        isInputWrong = false;
                        break;
                    case "8":
                        Environment.Exit(0);
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

        // 1-1-1 화면: 개설 학과 전공으로 검색하여 수강 신청 
        public static void PrintScreen_1_1_1()
        {
            PrintTitle();
            Console.Write("개설 학과 전공으로 검색: ");
            
            List<Subject> searchedList = new List<Subject>();

            bool isThereSearchResult = false;
            while (!isThereSearchResult)
            {
                string input = Console.ReadLine();
                for (int i = 0; i < 20; i++)
                {
                    if (subjectList[i].major.Contains(input))
                    {
                        searchedList.Add(subjectList[i]);
                        isThereSearchResult = true;
                    }
                    else { }
                }

                if (isThereSearchResult != true)
                {
                    Console.WriteLine("검색 결과가 없습니다. 다시 검색해보세요: ");
                }
                else
                {
                    string formatTitle = "{0,-5}{1,-10}{2,-10}{3,-5}{4,-15}{5,-7}{6,-5}{7,-5}{8,-35}{9,-15}{10,-25}{11,-5}";
                    string formatContent = "{0,-5}{1,-10}{2,-10}{3,-5}{4,-15}{5,-7}{6,-5}{7,-5}{8,-35}{9,-15}{10,-25}{11,-5}";
                    Console.WriteLine(formatTitle, "No.", "개설학과전공", "학수번호", "분반", "교과목명", "이수구분", "학년", "학점", "요일 및 강의시간", "강의실", "메인교수명", "강의언어");
                    Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------");
                    for (int i = 0; i < searchedList.Count; i++)
                    {
                        Console.WriteLine(formatContent, searchedList[i].number, searchedList[i].major, searchedList[i].id, searchedList[i].group, searchedList[i].name, searchedList[i].division, searchedList[i].grade, searchedList[i].unit, searchedList[i].time, searchedList[i].classroom, searchedList[i].professor, searchedList[i].language);
                    }
                }
            }

            Console.Write("수강신청하고 싶은 과목의 번호(NO.)를 눌러주세요: ");
            bool isEnrolmentDone = false;
            while (!isEnrolmentDone)
            {
                bool enrolSuccess = false;
                string newinput = Console.ReadLine();
                for (int i = 0; i < searchedList.Count; i++)
                {
                    if (searchedList[i].number == newinput)
                    {
                        for (int j = 0; j < enrolmentList.Count; j++)
                        {
                            if (searchedList[i].id == enrolmentList[j].id)
                            {
                                Console.WriteLine("해당 학수번호의 과목은 이미 수강 신청되어있습니다. 다시 입력해보세요: ");
                                break;
                            }
                            else if (unitTotal + int.Parse(searchedList[i].unit) > 21)
                            {
                                Console.WriteLine("21학점을 초과합니다. 다시 입력해보세요: ");
                                break;
                            }
                            else
                            {
                                enrolSuccess = true;
                            }
                        }
                        if (enrolSuccess == true)
                        {
                            Console.WriteLine("해당 과목을 수강 신청했습니다.");
                            enrolmentList.Add(searchedList[i]);
                            isEnrolmentDone = true;
                            break;
                        }
                        else { }
                    }
                    else { }
                }

                if (isEnrolmentDone == false && enrolSuccess == false)
                {
                    Console.WriteLine("검색 결과에 해당 번호의 과목이 없습니다. 다시 입력해보세요: ");
                }
                else { }
            }

            Console.WriteLine("ESC를 눌러 메인화면으로 돌아가세요.");
            while (Console.ReadKey().Key != ConsoleKey.Escape) { }
            PrintMainScreen();

        }

        // 1-1-2 화면: 학수 번호 
        public static void PrintScreen_1_1_2()
        {
            PrintTitle();
            Console.Write("학수 번호로 검색: ");

            List<Subject> searchedList = new List<Subject>();

            bool isThereSearchResult = false;
            while (!isThereSearchResult)
            {
                string input = Console.ReadLine();
                for (int i = 0; i < 20; i++)
                {
                    if (subjectList[i].id.Contains(input))
                    {
                        searchedList.Add(subjectList[i]);
                        isThereSearchResult = true;
                    }
                    else { }
                }

                if (isThereSearchResult != true)
                {
                    Console.WriteLine("검색 결과가 없습니다. 다시 검색해보세요: ");
                }
                else
                {
                    string formatTitle = "{0,-5}{1,-10}{2,-10}{3,-5}{4,-15}{5,-7}{6,-5}{7,-5}{8,-35}{9,-15}{10,-25}{11,-5}";
                    string formatContent = "{0,-5}{1,-10}{2,-10}{3,-5}{4,-15}{5,-7}{6,-5}{7,-5}{8,-35}{9,-15}{10,-25}{11,-5}";
                    Console.WriteLine(formatTitle, "No.", "개설학과전공", "학수번호", "분반", "교과목명", "이수구분", "학년", "학점", "요일 및 강의시간", "강의실", "메인교수명", "강의언어");
                    Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------");
                    for (int i = 0; i < searchedList.Count; i++)
                    {
                        Console.WriteLine(formatContent, searchedList[i].number, searchedList[i].major, searchedList[i].id, searchedList[i].group, searchedList[i].name, searchedList[i].division, searchedList[i].grade, searchedList[i].unit, searchedList[i].time, searchedList[i].classroom, searchedList[i].professor, searchedList[i].language);
                    }
                }
            }

            Console.Write("수강신청하고 싶은 과목의 번호(NO.)를 눌러주세요: ");
            bool isEnrolmentDone = false;
            while (!isEnrolmentDone)
            {
                bool enrolledAlready = false;
                string newinput = Console.ReadLine();
                for (int i = 0; i < searchedList.Count; i++)
                {
                    if (searchedList[i].number == newinput)
                    {
                        for (int j=0; j< enrolmentList.Count; j++)
                        {
                            if (searchedList[i].id == enrolmentList[j].id)
                            {
                                Console.WriteLine("해당 학수번호의 과목은 이미 수강 신청되어있습니다. 다시 입력해보세요: ");
                                enrolledAlready = true;
                                break;
                            }
                            else { }
                        }
                        if (enrolledAlready == false)
                        {
                            Console.WriteLine("해당 과목을 수강 신청했습니다.");
                            enrolmentList.Add(searchedList[i]);
                            isEnrolmentDone = true;
                            break;
                        }
                        else { }
                    }
                    else { }
                }

                if (isEnrolmentDone == false && enrolledAlready == false)
                {
                    Console.WriteLine("검색 결과에 해당 번호의 과목이 없습니다. 다시 입력해보세요: ");
                }
                else { }
            }

            Console.WriteLine("ESC를 눌러 메인화면으로 돌아가세요.");
            while (Console.ReadKey().Key != ConsoleKey.Escape) { }
            PrintMainScreen();

        }

        // 1-1-3 화면: 교과목 명 
        public static void PrintScreen_1_1_3()
        {
            PrintTitle();
            Console.Write("교과목 명으로 검색: ");

            List<Subject> searchedList = new List<Subject>();

            bool isThereSearchResult = false;
            while (!isThereSearchResult)
            {
                string input = Console.ReadLine();
                for (int i = 0; i < 20; i++)
                {
                    if (subjectList[i].name.Contains(input))
                    {
                        searchedList.Add(subjectList[i]);
                        isThereSearchResult = true;
                    }
                    else { }
                }

                if (isThereSearchResult != true)
                {
                    Console.WriteLine("검색 결과가 없습니다. 다시 검색해보세요: ");
                }
                else
                {
                    string formatTitle = "{0,-5}{1,-10}{2,-10}{3,-5}{4,-15}{5,-7}{6,-5}{7,-5}{8,-35}{9,-15}{10,-25}{11,-5}";
                    string formatContent = "{0,-5}{1,-10}{2,-10}{3,-5}{4,-15}{5,-7}{6,-5}{7,-5}{8,-35}{9,-15}{10,-25}{11,-5}";
                    Console.WriteLine(formatTitle, "No.", "개설학과전공", "학수번호", "분반", "교과목명", "이수구분", "학년", "학점", "요일 및 강의시간", "강의실", "메인교수명", "강의언어");
                    Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------");
                    for (int i = 0; i < searchedList.Count; i++)
                    {
                        Console.WriteLine(formatContent, searchedList[i].number, searchedList[i].major, searchedList[i].id, searchedList[i].group, searchedList[i].name, searchedList[i].division, searchedList[i].grade, searchedList[i].unit, searchedList[i].time, searchedList[i].classroom, searchedList[i].professor, searchedList[i].language);
                    }
                }
            }

            Console.Write("수강신청하고 싶은 과목의 번호(NO.)를 눌러주세요: ");
            bool isEnrolmentDone = false;
            while (!isEnrolmentDone)
            {
                bool enrolledAlready = false;
                string newinput = Console.ReadLine();
                for (int i = 0; i < searchedList.Count; i++)
                {
                    if (searchedList[i].number == newinput)
                    {
                        for (int j = 0; j < enrolmentList.Count; j++)
                        {
                            if (searchedList[i].id == enrolmentList[j].id)
                            {
                                Console.WriteLine("해당 학수번호의 과목은 이미 수강 신청되어있습니다. 다시 입력해보세요: ");
                                enrolledAlready = true;
                                break;
                            }
                            else { }
                        }
                        if (enrolledAlready == false)
                        {
                            Console.WriteLine("해당 과목을 수강 신청했습니다.");
                            enrolmentList.Add(searchedList[i]);
                            isEnrolmentDone = true;
                            break;
                        }
                        else { }
                    }
                    else { }
                }

                if (isEnrolmentDone == false && enrolledAlready == false)
                {
                    Console.WriteLine("검색 결과에 해당 번호의 과목이 없습니다. 다시 입력해보세요: ");
                }
                else { }
            }

            Console.WriteLine("ESC를 눌러 메인화면으로 돌아가세요.");
            while (Console.ReadKey().Key != ConsoleKey.Escape) { }
            PrintMainScreen();

        }

        // 1-1-4 화면: 강의 대상 학년 
        public static void PrintScreen_1_1_4()
        {
            PrintTitle();
            Console.Write("강의 대상 학년으로 검색: ");

            List<Subject> searchedList = new List<Subject>();

            bool isThereSearchResult = false;
            while (!isThereSearchResult)
            {
                string input = Console.ReadLine();
                for (int i = 0; i < 20; i++)
                {
                    if (subjectList[i].grade.Contains(input))
                    {
                        searchedList.Add(subjectList[i]);
                        isThereSearchResult = true;
                    }
                    else { }
                }

                if (isThereSearchResult != true)
                {
                    Console.WriteLine("검색 결과가 없습니다. 다시 검색해보세요: ");
                }
                else
                {
                    string formatTitle = "{0,-5}{1,-10}{2,-10}{3,-5}{4,-15}{5,-7}{6,-5}{7,-5}{8,-35}{9,-15}{10,-25}{11,-5}";
                    string formatContent = "{0,-5}{1,-10}{2,-10}{3,-5}{4,-15}{5,-7}{6,-5}{7,-5}{8,-35}{9,-15}{10,-25}{11,-5}";
                    Console.WriteLine(formatTitle, "No.", "개설학과전공", "학수번호", "분반", "교과목명", "이수구분", "학년", "학점", "요일 및 강의시간", "강의실", "메인교수명", "강의언어");
                    Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------");
                    for (int i = 0; i < searchedList.Count; i++)
                    {
                        Console.WriteLine(formatContent, searchedList[i].number, searchedList[i].major, searchedList[i].id, searchedList[i].group, searchedList[i].name, searchedList[i].division, searchedList[i].grade, searchedList[i].unit, searchedList[i].time, searchedList[i].classroom, searchedList[i].professor, searchedList[i].language);
                    }
                }
            }

            Console.Write("수강신청하고 싶은 과목의 번호(NO.)를 눌러주세요: ");
            bool isEnrolmentDone = false;
            while (!isEnrolmentDone)
            {
                bool enrolledAlready = false;
                string newinput = Console.ReadLine();
                for (int i = 0; i < searchedList.Count; i++)
                {
                    if (searchedList[i].number == newinput)
                    {
                        for (int j = 0; j < enrolmentList.Count; j++)
                        {
                            if (searchedList[i].id == enrolmentList[j].id)
                            {
                                Console.WriteLine("해당 학수번호의 과목은 이미 수강 신청되어있습니다. 다시 입력해보세요: ");
                                enrolledAlready = true;
                                break;
                            }
                            else { }
                        }
                        if (enrolledAlready == false)
                        {
                            Console.WriteLine("해당 과목을 수강 신청했습니다.");
                            enrolmentList.Add(searchedList[i]);
                            isEnrolmentDone = true;
                            break;
                        }
                        else { }
                    }
                    else { }
                }

                if (isEnrolmentDone == false && enrolledAlready == false)
                {
                    Console.WriteLine("검색 결과에 해당 번호의 과목이 없습니다. 다시 입력해보세요: ");
                }
                else { }
            }

            Console.WriteLine("ESC를 눌러 메인화면으로 돌아가세요.");
            while (Console.ReadKey().Key != ConsoleKey.Escape) { }
            PrintMainScreen();

        }

        // 1-1-5 화면: 교수명 
        public static void PrintScreen_1_1_5()
        {
            PrintTitle();
            Console.Write("교수명으로 검색: ");

            List<Subject> searchedList = new List<Subject>();

            bool isThereSearchResult = false;
            while (!isThereSearchResult)
            {
                string input = Console.ReadLine();
                for (int i = 0; i < 20; i++)
                {
                    if (subjectList[i].professor.Contains(input))
                    {
                        searchedList.Add(subjectList[i]);
                        isThereSearchResult = true;
                    }
                    else { }
                }

                if (isThereSearchResult != true)
                {
                    Console.WriteLine("검색 결과가 없습니다. 다시 검색해보세요: ");
                }
                else
                {
                    string formatTitle = "{0,-5}{1,-10}{2,-10}{3,-5}{4,-15}{5,-7}{6,-5}{7,-5}{8,-35}{9,-15}{10,-25}{11,-5}";
                    string formatContent = "{0,-5}{1,-10}{2,-10}{3,-5}{4,-15}{5,-7}{6,-5}{7,-5}{8,-35}{9,-15}{10,-25}{11,-5}";
                    Console.WriteLine(formatTitle, "No.", "개설학과전공", "학수번호", "분반", "교과목명", "이수구분", "학년", "학점", "요일 및 강의시간", "강의실", "메인교수명", "강의언어");
                    Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------");
                    for (int i = 0; i < searchedList.Count; i++)
                    {
                        Console.WriteLine(formatContent, searchedList[i].number, searchedList[i].major, searchedList[i].id, searchedList[i].group, searchedList[i].name, searchedList[i].division, searchedList[i].grade, searchedList[i].unit, searchedList[i].time, searchedList[i].classroom, searchedList[i].professor, searchedList[i].language);
                    }
                }
            }

            Console.Write("수강신청하고 싶은 과목의 번호(NO.)를 눌러주세요: ");
            bool isEnrolmentDone = false;
            while (!isEnrolmentDone)
            {
                bool enrolledAlready = false;
                string newinput = Console.ReadLine();
                for (int i = 0; i < searchedList.Count; i++)
                {
                    if (searchedList[i].number == newinput)
                    {
                        for (int j = 0; j < enrolmentList.Count; j++)
                        {
                            if (searchedList[i].id == enrolmentList[j].id)
                            {
                                Console.WriteLine("해당 학수번호의 과목은 이미 수강 신청되어있습니다. 다시 입력해보세요: ");
                                enrolledAlready = true;
                                break;
                            }
                            else { }
                        }
                        if (enrolledAlready == false)
                        {
                            Console.WriteLine("해당 과목을 수강 신청했습니다.");
                            enrolmentList.Add(searchedList[i]);
                            isEnrolmentDone = true;
                            break;
                        }
                        else { }
                    }
                    else { }
                }

                if (isEnrolmentDone == false && enrolledAlready == false)
                {
                    Console.WriteLine("검색 결과에 해당 번호의 과목이 없습니다. 다시 입력해보세요: ");
                }
                else { }
            }

            Console.WriteLine("ESC를 눌러 메인화면으로 돌아가세요.");
            while (Console.ReadKey().Key != ConsoleKey.Escape) { }
            PrintMainScreen();

        }

        // 1-1-6 화면: 관심 과목 
        public static void PrintScreen_1_1_6()
        {

        }


        // 1-2 화면: 수강 강의 삭제 
        public static void PrintScreen_1_2()
        {

        }

        // 1-3 화면: 수강 강의 조회 
        public static void PrintScreen_1_3()
        {

        }

        // 1-4 화면: 전체 강의 목록
        public static void PrintScreen_1_4()
        {

        }

        // 1-5 화면: 강의 검
        public static void PrintScreen_1_5()
        {

        }

        // 1-6 화면
        public static void PrintScreen_1_6()
        {

        }

        // 2번 화면 
        public static void PrintScreen_2()
        {

        }

        // 3번 화면 
        public static void PrintScreen_3()
        {

        }

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
                    DisassembleSubjects(subjectTextList[i]);
                    //Console.WriteLine(subjectList[i - 1].name);
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


