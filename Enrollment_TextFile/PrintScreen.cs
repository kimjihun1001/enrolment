using System;
using System.Collections.Generic;
using System.IO;

namespace Enrollment_TextFile
{
    public class PrintScreen
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
        public static double SumTotalUnit()
        {
            double unitTotal = 0;
            for (int i = 0; i < enrolmentList.Count; i++)
            {
                unitTotal += double.Parse(enrolmentList[i].unit);
            }
            return unitTotal;
        }

        //// 총 학점 -> 아 이렇게 변수로 사용하면 안되고, 그냥 항상 불러와야되는구나 그래야 업데이트된 총 학점이 반영됨 
        //public static int unitTotal = SumTotalUnit();

        // 관심 과목 추가한 목록을 담아둘 리스트 생성 
        public static List<Subject> interestList = new List<Subject>();

        // 관심 과목 추가된 과목의 총 학점 계산 함수
        public static double SumTotalInterestUnit()
        {
            double unitTotal = 0;
            for (int i = 0; i < interestList.Count; i++)
            {
                unitTotal += double.Parse(interestList[i].unit);
            }
            return unitTotal;
        }

        // 해당 과목의 요일, 시간을 가져오는 함수 
        public static List<double> GetTime(Subject args)
        {
            List<double> dayAndTime = new List<double>();
            // 요일, 시간, 요일, 시간 
            string[] timeSplit1 = args.time.Split(' ');
            // 홀수번째에서 요일 가져오고, 짝수번째에서 시간 가져오기 
            for (int i = 0; i < timeSplit1.Length; i++)
            {
                switch (timeSplit1[i])
                {
                    case "월":
                        dayAndTime.Add(0);
                        break;
                    case "화":
                        dayAndTime.Add(1);
                        break;
                    case "수":
                        dayAndTime.Add(2);
                        break;
                    case "목":
                        dayAndTime.Add(3);
                        break;
                    case "금":
                        dayAndTime.Add(4);
                        break;
                    default:
                        break;
                }

                i++;
                string[] timeSplit2 = timeSplit1[i].Split('~');
                string[] timeSplit3 = timeSplit2[0].Split(':');
                string[] timeSplit4 = timeSplit2[1].Split(':');

                for (int j = 0; j < 2; j++)
                {
                    if (timeSplit3[j] == "00")
                    {
                        timeSplit3[j] = "0";
                    }
                    else if (timeSplit3[j] == "30")
                    {
                        timeSplit3[j] = "0.5";
                    }
                    else { }

                    if (timeSplit4[j] == "00")
                    {
                        timeSplit4[j] = "0";
                    }
                    else if (timeSplit4[j] == "30")
                    {
                        timeSplit4[j] = "0.5";
                    }
                    else { }
                }
                double startTime = int.Parse(timeSplit3[0]) + double.Parse(timeSplit3[1]);
                double endTime = int.Parse(timeSplit4[0]) + double.Parse(timeSplit4[1]);
                dayAndTime.Add(startTime);
                dayAndTime.Add(endTime);
            }

            // 요일, 시작시간, 끝시간, ... 
            return dayAndTime;
        }

        // 두 과목의 수업 시간이 겹치는지 비교하는 함수
        public static bool CheckTime(Subject args, Subject args2)
        {
            bool isOverlapped = false;
            for (int i = 0; i < GetTime(args).Count; i += 3)
            {
                for (int j = 0; j < GetTime(args2).Count; j += 3)
                {
                    if (GetTime(args)[i] == GetTime(args2)[j])
                    {
                        // 겹치면 아웃 
                        if ((GetTime(args)[i + 2] > GetTime(args2)[j + 1]) && (GetTime(args)[i + 1] < GetTime(args2)[j + 2]))
                        {
                            isOverlapped = true;
                        }
                        else { }
                    }
                    else { }
                }
            }

            return isOverlapped;
        }

        // 과목 리스트 출력 함수
        public static void PrintList(List<Subject> subjects)
        {
            int row = 12;
            Console.SetCursorPosition(0, row);
            Console.Write("No.");
            Console.SetCursorPosition(5, row);
            Console.Write("개설학과전공");
            Console.SetCursorPosition(23, row);
            Console.Write("학수번호");
            Console.SetCursorPosition(33, row);
            Console.Write("분반");
            Console.SetCursorPosition(38, row);
            Console.Write("교과목명");
            Console.SetCursorPosition(61, row);
            Console.Write("이수구분");
            Console.SetCursorPosition(71, row);
            Console.Write("학년");
            Console.SetCursorPosition(75, row);
            Console.Write("학점");
            Console.SetCursorPosition(81, row);
            Console.Write("요일 및 강의시간");
            Console.SetCursorPosition(126, row);
            Console.Write("강의실");
            Console.SetCursorPosition(142, row);
            Console.Write("교수명");
            Console.SetCursorPosition(166, row);
            Console.Write("강의언어");
            Console.SetCursorPosition(176, row);
            Console.Write("수강");
            Console.SetCursorPosition(182, row);
            Console.WriteLine("관심");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");

            for (int i = 0; i < subjects.Count; i++)
            {
                row = i + 14;
                Console.SetCursorPosition(0, row);
                Console.Write(subjects[i].number);
                Console.SetCursorPosition(5, row);
                Console.Write(subjects[i].major);
                Console.SetCursorPosition(23, row);
                Console.Write(subjects[i].id);
                Console.SetCursorPosition(33, row);
                Console.Write(subjects[i].group);
                Console.SetCursorPosition(38, row);
                Console.Write(subjects[i].name);
                Console.SetCursorPosition(61, row);
                Console.Write(subjects[i].division);
                Console.SetCursorPosition(71, row);
                Console.Write(subjects[i].grade);
                Console.SetCursorPosition(75, row);
                Console.Write(subjects[i].unit);
                Console.SetCursorPosition(81, row);
                Console.Write(subjects[i].time);
                Console.SetCursorPosition(126, row);
                Console.Write(subjects[i].classroom);
                Console.SetCursorPosition(142, row);
                Console.Write(subjects[i].professor);
                Console.SetCursorPosition(166, row);
                Console.Write(subjects[i].language);
                Console.SetCursorPosition(176, row);
                Console.Write(subjects[i].enrollment);
                Console.SetCursorPosition(182, row);
                Console.WriteLine(subjects[i].interest);
            }
        }

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
                    PrintList(searchedList);
                }
            }

            Console.Write("수강신청하고 싶은 과목의 번호(NO.)를 눌러주세요: ");
            bool isEnrolmentDone = false;
            while (!isEnrolmentDone)
            {
                bool enrolSuccess = true;
                bool isNewinputValid = false;
                string newinput = Console.ReadLine();
                for (int i = 0; i < searchedList.Count; i++)
                {
                    if (searchedList[i].number == newinput)
                    {
                        // 검색 결과 내에 입력한 값이 있는데 오류가 발생한 경우이기 때문에 true로 바꿔준다 
                        isNewinputValid = true;

                        if (SumTotalUnit() + double.Parse(searchedList[i].unit) > 21)
                        {
                            Console.WriteLine("21학점을 초과합니다. 다시 입력해보세요: ");
                            enrolSuccess = false;
                            break;
                        }
                        else
                        {
                            for (int j = 0; j < enrolmentList.Count; j++)
                            {
                                if (searchedList[i].id == enrolmentList[j].id)
                                {
                                    Console.WriteLine("해당 학수번호의 과목은 이미 수강 신청되어있습니다. 다시 입력해보세요: ");
                                    enrolSuccess = false;
                                    break;
                                }
                                else if (CheckTime(searchedList[i], enrolmentList[j]))
                                {
                                    Console.WriteLine("기존 신청 과목과 시간이 겹칩니다. 다시 입력해보세요: ");
                                    enrolSuccess = false;
                                    break;
                                }
                                else { }
                            }
                        }

                        if (enrolSuccess == true)
                        {
                            Console.WriteLine("해당 과목을 수강 신청했습니다.");
                            // 검색 리스트의 번호와 같은 과목 리스트의 수강 신청 여부를 업데이트하고 수강 신청 리스트에 추가 
                            for (int j = 0; j < subjectList.Count; j++)
                            {
                                if (searchedList[i].number == subjectList[j].number)
                                {
                                    subjectList[j].enrollment = "O";
                                    enrolmentList.Add(subjectList[j]);
                                    break;
                                }
                                else { }
                            }
                            isEnrolmentDone = true;
                            break;
                        }
                        else { }
                    }
                    else { }
                }

                if (isEnrolmentDone == false && enrolSuccess == false && isNewinputValid == false)
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
                    PrintList(searchedList);
                }
            }

            Console.Write("수강신청하고 싶은 과목의 번호(NO.)를 눌러주세요: ");
            bool isEnrolmentDone = false;
            while (!isEnrolmentDone)
            {
                bool enrolSuccess = true;
                bool isNewinputValid = false;
                string newinput = Console.ReadLine();
                for (int i = 0; i < searchedList.Count; i++)
                {
                    if (searchedList[i].number == newinput)
                    {
                        // 검색 결과 내에 입력한 값이 있는데 오류가 발생한 경우이기 때문에 true로 바꿔준다 
                        isNewinputValid = true;

                        if (SumTotalUnit() + double.Parse(searchedList[i].unit) > 21)
                        {
                            Console.WriteLine("21학점을 초과합니다. 다시 입력해보세요: ");
                            enrolSuccess = false;
                            break;
                        }
                        else
                        {
                            for (int j = 0; j < enrolmentList.Count; j++)
                            {
                                if (searchedList[i].id == enrolmentList[j].id)
                                {
                                    Console.WriteLine("해당 학수번호의 과목은 이미 수강 신청되어있습니다. 다시 입력해보세요: ");
                                    enrolSuccess = false;
                                    break;
                                }
                                else if (CheckTime(searchedList[i], enrolmentList[j]))
                                {
                                    Console.WriteLine("기존 신청 과목과 시간이 겹칩니다. 다시 입력해보세요: ");
                                    enrolSuccess = false;
                                    break;
                                }
                                else { }
                            }
                        }

                        if (enrolSuccess == true)
                        {
                            Console.WriteLine("해당 과목을 수강 신청했습니다.");
                            // 검색 리스트의 번호와 같은 과목 리스트의 수강 신청 여부를 업데이트하고 수강 신청 리스트에 추가 
                            for (int j = 0; j < subjectList.Count; j++)
                            {
                                if (searchedList[i].number == subjectList[j].number)
                                {
                                    subjectList[j].enrollment = "O";
                                    enrolmentList.Add(subjectList[j]);
                                    break;
                                }
                                else { }
                            }
                            isEnrolmentDone = true;
                            break;
                        }
                        else { }
                    }
                    else { }
                }

                if (isEnrolmentDone == false && enrolSuccess == false && isNewinputValid == false)
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
                    PrintList(searchedList);
                }
            }

            Console.Write("수강신청하고 싶은 과목의 번호(NO.)를 눌러주세요: ");
            bool isEnrolmentDone = false;
            while (!isEnrolmentDone)
            {
                bool enrolSuccess = true;
                bool isNewinputValid = false;
                string newinput = Console.ReadLine();
                for (int i = 0; i < searchedList.Count; i++)
                {
                    if (searchedList[i].number == newinput)
                    {
                        // 검색 결과 내에 입력한 값이 있는데 오류가 발생한 경우이기 때문에 true로 바꿔준다 
                        isNewinputValid = true;

                        if (SumTotalUnit() + double.Parse(searchedList[i].unit) > 21)
                        {
                            Console.WriteLine("21학점을 초과합니다. 다시 입력해보세요: ");
                            enrolSuccess = false;
                            break;
                        }
                        else
                        {
                            for (int j = 0; j < enrolmentList.Count; j++)
                            {
                                if (searchedList[i].id == enrolmentList[j].id)
                                {
                                    Console.WriteLine("해당 학수번호의 과목은 이미 수강 신청되어있습니다. 다시 입력해보세요: ");
                                    enrolSuccess = false;
                                    break;
                                }
                                else if (CheckTime(searchedList[i], enrolmentList[j]))
                                {
                                    Console.WriteLine("기존 신청 과목과 시간이 겹칩니다. 다시 입력해보세요: ");
                                    enrolSuccess = false;
                                    break;
                                }
                                else { }
                            }
                        }

                        if (enrolSuccess == true)
                        {
                            Console.WriteLine("해당 과목을 수강 신청했습니다.");
                            // 검색 리스트의 번호와 같은 과목 리스트의 수강 신청 여부를 업데이트하고 수강 신청 리스트에 추가 
                            for (int j = 0; j < subjectList.Count; j++)
                            {
                                if (searchedList[i].number == subjectList[j].number)
                                {
                                    subjectList[j].enrollment = "O";
                                    enrolmentList.Add(subjectList[j]);
                                    break;
                                }
                                else { }
                            }
                            isEnrolmentDone = true;
                            break;
                        }
                        else { }
                    }
                    else { }
                }

                if (isEnrolmentDone == false && enrolSuccess == false && isNewinputValid == false)
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
                    PrintList(searchedList);
                }
            }

            Console.Write("수강신청하고 싶은 과목의 번호(NO.)를 눌러주세요: ");
            bool isEnrolmentDone = false;
            while (!isEnrolmentDone)
            {
                bool enrolSuccess = true;
                bool isNewinputValid = false;
                string newinput = Console.ReadLine();
                for (int i = 0; i < searchedList.Count; i++)
                {
                    if (searchedList[i].number == newinput)
                    {
                        // 검색 결과 내에 입력한 값이 있는데 오류가 발생한 경우이기 때문에 true로 바꿔준다 
                        isNewinputValid = true;

                        if (SumTotalUnit() + double.Parse(searchedList[i].unit) > 21)
                        {
                            Console.WriteLine("21학점을 초과합니다. 다시 입력해보세요: ");
                            enrolSuccess = false;
                            break;
                        }
                        else
                        {
                            for (int j = 0; j < enrolmentList.Count; j++)
                            {
                                if (searchedList[i].id == enrolmentList[j].id)
                                {
                                    Console.WriteLine("해당 학수번호의 과목은 이미 수강 신청되어있습니다. 다시 입력해보세요: ");
                                    enrolSuccess = false;
                                    break;
                                }
                                else if (CheckTime(searchedList[i], enrolmentList[j]))
                                {
                                    Console.WriteLine("기존 신청 과목과 시간이 겹칩니다. 다시 입력해보세요: ");
                                    enrolSuccess = false;
                                    break;
                                }
                                else { }
                            }
                        }

                        if (enrolSuccess == true)
                        {
                            Console.WriteLine("해당 과목을 수강 신청했습니다.");
                            // 검색 리스트의 번호와 같은 과목 리스트의 수강 신청 여부를 업데이트하고 수강 신청 리스트에 추가 
                            for (int j = 0; j < subjectList.Count; j++)
                            {
                                if (searchedList[i].number == subjectList[j].number)
                                {
                                    subjectList[j].enrollment = "O";
                                    enrolmentList.Add(subjectList[j]);
                                    break;
                                }
                                else { }
                            }
                            isEnrolmentDone = true;
                            break;
                        }
                        else { }
                    }
                    else { }
                }

                if (isEnrolmentDone == false && enrolSuccess == false && isNewinputValid == false)
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
                    PrintList(searchedList);
                }
            }

            Console.Write("수강신청하고 싶은 과목의 번호(NO.)를 눌러주세요: ");
            bool isEnrolmentDone = false;
            while (!isEnrolmentDone)
            {
                bool enrolSuccess = true;
                bool isNewinputValid = false;
                string newinput = Console.ReadLine();
                for (int i = 0; i < searchedList.Count; i++)
                {
                    if (searchedList[i].number == newinput)
                    {
                        // 검색 결과 내에 입력한 값이 있는데 오류가 발생한 경우이기 때문에 true로 바꿔준다 
                        isNewinputValid = true;

                        if (SumTotalUnit() + double.Parse(searchedList[i].unit) > 21)
                        {
                            Console.WriteLine("21학점을 초과합니다. 다시 입력해보세요: ");
                            enrolSuccess = false;
                            break;
                        }
                        else
                        {
                            for (int j = 0; j < enrolmentList.Count; j++)
                            {
                                if (searchedList[i].id == enrolmentList[j].id)
                                {
                                    Console.WriteLine("해당 학수번호의 과목은 이미 수강 신청되어있습니다. 다시 입력해보세요: ");
                                    enrolSuccess = false;
                                    break;
                                }
                                else if (CheckTime(searchedList[i], enrolmentList[j]))
                                {
                                    Console.WriteLine("기존 신청 과목과 시간이 겹칩니다. 다시 입력해보세요: ");
                                    enrolSuccess = false;
                                    break;
                                }
                                else { }
                            }
                        }

                        if (enrolSuccess == true)
                        {
                            Console.WriteLine("해당 과목을 수강 신청했습니다.");
                            // 검색 리스트의 번호와 같은 과목 리스트의 수강 신청 여부를 업데이트하고 수강 신청 리스트에 추가 
                            for (int j = 0; j < subjectList.Count; j++)
                            {
                                if (searchedList[i].number == subjectList[j].number)
                                {
                                    subjectList[j].enrollment = "O";
                                    enrolmentList.Add(subjectList[j]);
                                    break;
                                }
                                else { }
                            }
                            isEnrolmentDone = true;
                            break;
                        }
                        else { }
                    }
                    else { }
                }

                if (isEnrolmentDone == false && enrolSuccess == false && isNewinputValid == false)
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
            PrintTitle();

            PrintList(interestList);

            Console.Write("수강신청하고 싶은 과목의 번호(NO.)를 눌러주세요: ");
            bool isEnrolmentDone = false;
            while (!isEnrolmentDone)
            {
                bool enrolSuccess = true;
                bool isNewinputValid = false;
                string newinput = Console.ReadLine();
                for (int i = 0; i < interestList.Count; i++)
                {
                    if (interestList[i].number == newinput)
                    {
                        // 검색 결과 내에 입력한 값이 있는데 오류가 발생한 경우이기 때문에 true로 바꿔준다 
                        isNewinputValid = true;

                        if (SumTotalUnit() + double.Parse(interestList[i].unit) > 21)
                        {
                            Console.WriteLine("21학점을 초과합니다. 다시 입력해보세요: ");
                            enrolSuccess = false;
                            break;
                        }
                        else
                        {
                            for (int j = 0; j < enrolmentList.Count; j++)
                            {
                                if (interestList[i].id == enrolmentList[j].id)
                                {
                                    Console.WriteLine("해당 학수번호의 과목은 이미 수강 신청되어있습니다. 다시 입력해보세요: ");
                                    enrolSuccess = false;
                                    break;
                                }
                                else if (CheckTime(interestList[i], enrolmentList[j]))
                                {
                                    Console.WriteLine("기존 신청 과목과 시간이 겹칩니다. 다시 입력해보세요: ");
                                    enrolSuccess = false;
                                    break;
                                }
                                else { }
                            }
                        }

                        if (enrolSuccess == true)
                        {
                            Console.WriteLine("해당 과목을 수강 신청했습니다.");
                            // 검색 리스트의 번호와 같은 과목 리스트의 수강 신청 여부를 업데이트하고 수강 신청 리스트에 추가 
                            for (int j = 0; j < subjectList.Count; j++)
                            {
                                if (interestList[i].number == subjectList[j].number)
                                {
                                    subjectList[j].enrollment = "O";
                                    enrolmentList.Add(subjectList[j]);
                                    break;
                                }
                                else { }
                            }
                            isEnrolmentDone = true;
                            break;
                        }
                        else { }
                    }
                    else { }
                }

                if (isEnrolmentDone == false && enrolSuccess == false && isNewinputValid == false)
                {
                    Console.WriteLine("검색 결과에 해당 번호의 과목이 없습니다. 다시 입력해보세요: ");
                }
                else { }
            }

            Console.WriteLine("ESC를 눌러 메인화면으로 돌아가세요.");
            while (Console.ReadKey().Key != ConsoleKey.Escape) { }
            PrintMainScreen();
        }


        // 1-2 화면: 수강 강의 삭제 
        public static void PrintScreen_1_2()
        {
            PrintTitle();

            PrintList(enrolmentList);

            Console.WriteLine();
            Console.WriteLine("내가 수강 신청한 총 학점: " + SumTotalUnit());
            Console.WriteLine();
            Console.Write("삭제하길 원하는 강의의 번호(No.)를 입력하세요: ");
            bool isDeleteDone = false;
            while (!isDeleteDone)
            {
                string input = Console.ReadLine();
                bool isInputValid = false;
                for (int i = 0; i < enrolmentList.Count; i++)
                {
                    if (input == enrolmentList[i].number)
                    {
                        Console.WriteLine("해당 과목이 삭제되었습니다.");
                        for (int j = 0; j < subjectList.Count; j++)
                        {
                            if (enrolmentList[i].number == subjectList[j].number)
                            {
                                subjectList[j].enrollment = "";
                                enrolmentList.RemoveAt(i);
                                break;
                            }
                            else { }
                        }
                        isInputValid = true;
                        isDeleteDone = true;
                        break;
                    }
                    else { }
                }
                if (isInputValid == false)
                {
                    Console.Write("수강 신청 목록 중에 입력하신 번호에 해당하는 과목이 없습니다. 다시 입력하세요: ");
                }
                else { }
            }
            Console.WriteLine("삭제를 계속 하시겠습니까? Y/N");
            bool isDeleteFinished = false;
            while (!isDeleteFinished)
            {
                string answer = Console.ReadLine();
                if (answer == "Y" || answer == "y")
                {
                    PrintScreen_1_2();
                    break;
                }
                else if (answer == "N" || answer == "n")
                {
                    PrintScreen_1();
                    break;
                }
                else
                {
                    Console.WriteLine("다시 입력하세요: ");
                }
            }
        }

        // 1-3 화면: 수강 강의 조회 
        public static void PrintScreen_1_3()
        {
            PrintTitle();

            PrintList(enrolmentList);

            Console.WriteLine();
            Console.WriteLine("내가 수강 신청한 총 학점: " + SumTotalUnit());
            Console.WriteLine("뒤로 돌아가려면 ESC를 누르세요.");
            while (Console.ReadKey().Key != ConsoleKey.Escape) { }
            PrintScreen_1();
        }

        // 1-4 화면: 전체 강의 목록
        public static void PrintScreen_1_4()
        {
            PrintTitle();

            PrintList(subjectList);

            Console.WriteLine();
            Console.WriteLine("뒤로 돌아가려면 ESC를 누르세요.");
            while (Console.ReadKey().Key != ConsoleKey.Escape) { }
            PrintScreen_1();
        }

        // 1-5 화면: 강의 검색 
        public static void PrintScreen_1_5()
        {
            Console.Clear();
            Console.WriteLine("1. 개설 학과 전공으로 검색");
            Console.WriteLine("2. 학수 번호로 검색");
            Console.WriteLine("3. 교과목 명으로 검색");
            Console.WriteLine("4. 강의 대상 학년으로 검색");
            Console.WriteLine("5. 교수명으로 검색");
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
                        PrintScreen_1_5_1();
                        isInputWrong = false;
                        break;
                    case "2":
                        PrintScreen_1_5_2();
                        isInputWrong = false;
                        break;
                    case "3":
                        PrintScreen_1_5_3();
                        isInputWrong = false;
                        break;
                    case "4":
                        PrintScreen_1_5_4();
                        isInputWrong = false;
                        break;
                    case "5":
                        PrintScreen_1_5_5();
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

        // 1-5-1
        public static void PrintScreen_1_5_1()
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
                    PrintList(searchedList);
                }
            }
            Console.WriteLine();
            Console.WriteLine("뒤로 돌아가려면 ESC를 누르세요.");
            while (Console.ReadKey().Key != ConsoleKey.Escape) { }
            PrintScreen_1_5();
        }

        // 1-5-2
        public static void PrintScreen_1_5_2()
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
                    PrintList(searchedList);
                }
            }
            Console.WriteLine();
            Console.WriteLine("뒤로 돌아가려면 ESC를 누르세요.");
            while (Console.ReadKey().Key != ConsoleKey.Escape) { }
            PrintScreen_1_5();
        }

        // 1-5-3
        public static void PrintScreen_1_5_3()
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
                    PrintList(searchedList);
                }
            }
            Console.WriteLine();
            Console.WriteLine("뒤로 돌아가려면 ESC를 누르세요.");
            while (Console.ReadKey().Key != ConsoleKey.Escape) { }
            PrintScreen_1_5();
        }

        // 1-5-4
        public static void PrintScreen_1_5_4()
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
                    PrintList(searchedList);
                }
            }
            Console.WriteLine();
            Console.WriteLine("뒤로 돌아가려면 ESC를 누르세요.");
            while (Console.ReadKey().Key != ConsoleKey.Escape) { }
            PrintScreen_1_5();
        }

        // 1-5-5
        public static void PrintScreen_1_5_5()
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
                    PrintList(searchedList);
                }
            }
            Console.WriteLine();
            Console.WriteLine("뒤로 돌아가려면 ESC를 누르세요.");
            while (Console.ReadKey().Key != ConsoleKey.Escape) { }
            PrintScreen_1_5();
        }













        // 2번 화면: 관심 과목
        public static void PrintScreen_2()
        {
            PrintTitle();
            Console.WriteLine("1. 관심 과목 추가");
            Console.WriteLine("2. 관심 과목 삭제");
            Console.WriteLine("3. 관심 과목 조회");
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
                        PrintScreen_2_1();
                        isInputWrong = false;
                        break;
                    case "2":
                        PrintScreen_2_2();
                        isInputWrong = false;
                        break;
                    case "3":
                        PrintScreen_2_3();
                        isInputWrong = false;
                        break;
                    case "4":
                        PrintScreen_2_4();
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

        // 2-1 화면: 관심 과목 추가 
        public static void PrintScreen_2_1()
        {
            Console.Clear();
            Console.WriteLine("1. 개설 학과 전공으로 검색하여 관심 과목 추가");
            Console.WriteLine("2. 학수 번호로 검색하여 관심 과목 추가");
            Console.WriteLine("3. 교과목 명으로 검색하여 관심 과목 추가");
            Console.WriteLine("4. 강의 대상 학년으로 검색하여 관심 과목 추가");
            Console.WriteLine("5. 교수명으로 검색하여 관심 과목 추가");
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
                        PrintScreen_2_1_1();
                        isInputWrong = false;
                        break;
                    case "2":
                        PrintScreen_2_1_2();
                        isInputWrong = false;
                        break;
                    case "3":
                        PrintScreen_2_1_3();
                        isInputWrong = false;
                        break;
                    case "4":
                        PrintScreen_2_1_4();
                        isInputWrong = false;
                        break;
                    case "5":
                        PrintScreen_2_1_5();
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

        // 2-1-1 화면: 개설 학과 전공으로 검색하여 관심 과목 추가
        public static void PrintScreen_2_1_1()
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
                    PrintList(searchedList);
                }
            }

            Console.Write("관심 과목으로 추가하고 싶은 과목의 번호(NO.)를 눌러주세요: ");
            bool isinterestDone = false;
            while (!isinterestDone)
            {
                bool interestSuccess = true;
                bool isNewinputValid = false;
                string newinput = Console.ReadLine();
                for (int i = 0; i < searchedList.Count; i++)
                {
                    if (searchedList[i].number == newinput)
                    {
                        // 검색 결과 내에 입력한 값이 있는데 오류가 발생한 경우이기 때문에 true로 바꿔준다 
                        isNewinputValid = true;

                        if (SumTotalInterestUnit() + double.Parse(searchedList[i].unit) > 24)
                        {
                            Console.WriteLine("24학점을 초과합니다. 다시 입력해보세요: ");
                            interestSuccess = false;
                            break;
                        }
                        else
                        {
                            for (int j = 0; j < interestList.Count; j++)
                            {
                                if (searchedList[i].id == enrolmentList[j].id)
                                {
                                    Console.WriteLine("해당 학수번호의 과목은 이미 관심 과목에 추가되어있습니다. 다시 입력해보세요: ");
                                    interestSuccess = false;
                                    break;
                                }
                                else if (CheckTime(searchedList[i], interestList[j]))
                                {
                                    Console.WriteLine("기존 관심 과목과 시간이 겹칩니다. 다시 입력해보세요: ");
                                    interestSuccess = false;
                                    break;
                                }
                                else { }
                            }
                        }

                        if (interestSuccess == true)
                        {
                            Console.WriteLine("해당 과목을 관심 과목으로 추가했습니다.");
                            // 검색 리스트의 번호와 같은 과목 리스트의 수강 신청 여부를 업데이트하고 수강 신청 리스트에 추가 
                            for (int j = 0; j < subjectList.Count; j++)
                            {
                                if (searchedList[i].number == subjectList[j].number)
                                {
                                    subjectList[j].interest = "O";
                                    interestList.Add(subjectList[j]);
                                    break;
                                }
                                else { }
                            }
                            isinterestDone = true;
                            break;
                        }
                        else { }
                    }
                    else { }
                }

                if (isinterestDone == false && interestSuccess == false && isNewinputValid == false)
                {
                    Console.WriteLine("검색 결과에 해당 번호의 과목이 없습니다. 다시 입력해보세요: ");
                }
                else { }
            }

            Console.WriteLine("ESC를 눌러 메인화면으로 돌아가세요.");
            while (Console.ReadKey().Key != ConsoleKey.Escape) { }
            PrintMainScreen();

        }

        // 2-1-2 화면: 학수 번호 
        public static void PrintScreen_2_1_2()
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
                    PrintList(searchedList);
                }
            }

            Console.Write("관심 과목으로 추가하고 싶은 과목의 번호(NO.)를 눌러주세요: ");
            bool isinterestDone = false;
            while (!isinterestDone)
            {
                bool interestSuccess = true;
                bool isNewinputValid = false;
                string newinput = Console.ReadLine();
                for (int i = 0; i < searchedList.Count; i++)
                {
                    if (searchedList[i].number == newinput)
                    {
                        // 검색 결과 내에 입력한 값이 있는데 오류가 발생한 경우이기 때문에 true로 바꿔준다 
                        isNewinputValid = true;

                        if (SumTotalInterestUnit() + double.Parse(searchedList[i].unit) > 24)
                        {
                            Console.WriteLine("24학점을 초과합니다. 다시 입력해보세요: ");
                            interestSuccess = false;
                            break;
                        }
                        else
                        {
                            for (int j = 0; j < interestList.Count; j++)
                            {
                                if (searchedList[i].id == enrolmentList[j].id)
                                {
                                    Console.WriteLine("해당 학수번호의 과목은 이미 관심 과목에 추가되어있습니다. 다시 입력해보세요: ");
                                    interestSuccess = false;
                                    break;
                                }
                                else if (CheckTime(searchedList[i], interestList[j]))
                                {
                                    Console.WriteLine("기존 관심 과목과 시간이 겹칩니다. 다시 입력해보세요: ");
                                    interestSuccess = false;
                                    break;
                                }
                                else { }
                            }
                        }

                        if (interestSuccess == true)
                        {
                            Console.WriteLine("해당 과목을 관심 과목으로 추가했습니다.");
                            // 검색 리스트의 번호와 같은 과목 리스트의 수강 신청 여부를 업데이트하고 수강 신청 리스트에 추가 
                            for (int j = 0; j < subjectList.Count; j++)
                            {
                                if (searchedList[i].number == subjectList[j].number)
                                {
                                    subjectList[j].interest = "O";
                                    interestList.Add(subjectList[j]);
                                    break;
                                }
                                else { }
                            }
                            isinterestDone = true;
                            break;
                        }
                        else { }
                    }
                    else { }
                }

                if (isinterestDone == false && interestSuccess == false && isNewinputValid == false)
                {
                    Console.WriteLine("검색 결과에 해당 번호의 과목이 없습니다. 다시 입력해보세요: ");
                }
                else { }
            }

            Console.WriteLine("ESC를 눌러 메인화면으로 돌아가세요.");
            while (Console.ReadKey().Key != ConsoleKey.Escape) { }
            PrintMainScreen();

        }

        // 2-1-3 화면: 교과목 명 
        public static void PrintScreen_2_1_3()
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
                    PrintList(searchedList);
                }
            }

            Console.Write("관심 과목으로 추가하고 싶은 과목의 번호(NO.)를 눌러주세요: ");
            bool isinterestDone = false;
            while (!isinterestDone)
            {
                bool interestSuccess = true;
                bool isNewinputValid = false;
                string newinput = Console.ReadLine();
                for (int i = 0; i < searchedList.Count; i++)
                {
                    if (searchedList[i].number == newinput)
                    {
                        // 검색 결과 내에 입력한 값이 있는데 오류가 발생한 경우이기 때문에 true로 바꿔준다 
                        isNewinputValid = true;

                        if (SumTotalInterestUnit() + double.Parse(searchedList[i].unit) > 24)
                        {
                            Console.WriteLine("24학점을 초과합니다. 다시 입력해보세요: ");
                            interestSuccess = false;
                            break;
                        }
                        else
                        {
                            for (int j = 0; j < interestList.Count; j++)
                            {
                                if (searchedList[i].id == enrolmentList[j].id)
                                {
                                    Console.WriteLine("해당 학수번호의 과목은 이미 관심 과목에 추가되어있습니다. 다시 입력해보세요: ");
                                    interestSuccess = false;
                                    break;
                                }
                                else if (CheckTime(searchedList[i], interestList[j]))
                                {
                                    Console.WriteLine("기존 관심 과목과 시간이 겹칩니다. 다시 입력해보세요: ");
                                    interestSuccess = false;
                                    break;
                                }
                                else { }
                            }
                        }

                        if (interestSuccess == true)
                        {
                            Console.WriteLine("해당 과목을 관심 과목으로 추가했습니다.");
                            // 검색 리스트의 번호와 같은 과목 리스트의 수강 신청 여부를 업데이트하고 수강 신청 리스트에 추가 
                            for (int j = 0; j < subjectList.Count; j++)
                            {
                                if (searchedList[i].number == subjectList[j].number)
                                {
                                    subjectList[j].interest = "O";
                                    interestList.Add(subjectList[j]);
                                    break;
                                }
                                else { }
                            }
                            isinterestDone = true;
                            break;
                        }
                        else { }
                    }
                    else { }
                }

                if (isinterestDone == false && interestSuccess == false && isNewinputValid == false)
                {
                    Console.WriteLine("검색 결과에 해당 번호의 과목이 없습니다. 다시 입력해보세요: ");
                }
                else { }
            }

            Console.WriteLine("ESC를 눌러 메인화면으로 돌아가세요.");
            while (Console.ReadKey().Key != ConsoleKey.Escape) { }
            PrintMainScreen();

        }

        // 2-1-4 화면: 강의 대상 학년 
        public static void PrintScreen_2_1_4()
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
                    PrintList(searchedList);
                }
            }

            Console.Write("관심 과목으로 추가하고 싶은 과목의 번호(NO.)를 눌러주세요: ");
            bool isinterestDone = false;
            while (!isinterestDone)
            {
                bool interestSuccess = true;
                bool isNewinputValid = false;
                string newinput = Console.ReadLine();
                for (int i = 0; i < searchedList.Count; i++)
                {
                    if (searchedList[i].number == newinput)
                    {
                        // 검색 결과 내에 입력한 값이 있는데 오류가 발생한 경우이기 때문에 true로 바꿔준다 
                        isNewinputValid = true;

                        if (SumTotalInterestUnit() + double.Parse(searchedList[i].unit) > 24)
                        {
                            Console.WriteLine("24학점을 초과합니다. 다시 입력해보세요: ");
                            interestSuccess = false;
                            break;
                        }
                        else
                        {
                            for (int j = 0; j < interestList.Count; j++)
                            {
                                if (searchedList[i].id == enrolmentList[j].id)
                                {
                                    Console.WriteLine("해당 학수번호의 과목은 이미 관심 과목에 추가되어있습니다. 다시 입력해보세요: ");
                                    interestSuccess = false;
                                    break;
                                }
                                else if (CheckTime(searchedList[i], interestList[j]))
                                {
                                    Console.WriteLine("기존 관심 과목과 시간이 겹칩니다. 다시 입력해보세요: ");
                                    interestSuccess = false;
                                    break;
                                }
                                else { }
                            }
                        }

                        if (interestSuccess == true)
                        {
                            Console.WriteLine("해당 과목을 관심 과목으로 추가했습니다.");
                            // 검색 리스트의 번호와 같은 과목 리스트의 수강 신청 여부를 업데이트하고 수강 신청 리스트에 추가 
                            for (int j = 0; j < subjectList.Count; j++)
                            {
                                if (searchedList[i].number == subjectList[j].number)
                                {
                                    subjectList[j].interest = "O";
                                    interestList.Add(subjectList[j]);
                                    break;
                                }
                                else { }
                            }
                            isinterestDone = true;
                            break;
                        }
                        else { }
                    }
                    else { }
                }

                if (isinterestDone == false && interestSuccess == false && isNewinputValid == false)
                {
                    Console.WriteLine("검색 결과에 해당 번호의 과목이 없습니다. 다시 입력해보세요: ");
                }
                else { }
            }

            Console.WriteLine("ESC를 눌러 메인화면으로 돌아가세요.");
            while (Console.ReadKey().Key != ConsoleKey.Escape) { }
            PrintMainScreen();

        }

        // 2-1-5 화면: 교수명 
        public static void PrintScreen_2_1_5()
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
                    PrintList(searchedList);
                }
            }

            Console.Write("관심 과목으로 추가하고 싶은 과목의 번호(NO.)를 눌러주세요: ");
            bool isinterestDone = false;
            while (!isinterestDone)
            {
                bool interestSuccess = true;
                bool isNewinputValid = false;
                string newinput = Console.ReadLine();
                for (int i = 0; i < searchedList.Count; i++)
                {
                    if (searchedList[i].number == newinput)
                    {
                        // 검색 결과 내에 입력한 값이 있는데 오류가 발생한 경우이기 때문에 true로 바꿔준다 
                        isNewinputValid = true;

                        if (SumTotalInterestUnit() + double.Parse(searchedList[i].unit) > 24)
                        {
                            Console.WriteLine("24학점을 초과합니다. 다시 입력해보세요: ");
                            interestSuccess = false;
                            break;
                        }
                        else
                        {
                            for (int j = 0; j < interestList.Count; j++)
                            {
                                if (searchedList[i].id == enrolmentList[j].id)
                                {
                                    Console.WriteLine("해당 학수번호의 과목은 이미 관심 과목에 추가되어있습니다. 다시 입력해보세요: ");
                                    interestSuccess = false;
                                    break;
                                }
                                else if (CheckTime(searchedList[i], interestList[j]))
                                {
                                    Console.WriteLine("기존 관심 과목과 시간이 겹칩니다. 다시 입력해보세요: ");
                                    interestSuccess = false;
                                    break;
                                }
                                else { }
                            }
                        }

                        if (interestSuccess == true)
                        {
                            Console.WriteLine("해당 과목을 관심 과목으로 추가했습니다.");
                            // 검색 리스트의 번호와 같은 과목 리스트의 수강 신청 여부를 업데이트하고 수강 신청 리스트에 추가 
                            for (int j = 0; j < subjectList.Count; j++)
                            {
                                if (searchedList[i].number == subjectList[j].number)
                                {
                                    subjectList[j].interest = "O";
                                    interestList.Add(subjectList[j]);
                                    break;
                                }
                                else { }
                            }
                            isinterestDone = true;
                            break;
                        }
                        else { }
                    }
                    else { }
                }

                if (isinterestDone == false && interestSuccess == false && isNewinputValid == false)
                {
                    Console.WriteLine("검색 결과에 해당 번호의 과목이 없습니다. 다시 입력해보세요: ");
                }
                else { }
            }

            Console.WriteLine("ESC를 눌러 메인화면으로 돌아가세요.");
            while (Console.ReadKey().Key != ConsoleKey.Escape) { }
            PrintMainScreen();

        }


        // 2-2 화면: 수강 강의 삭제 
        public static void PrintScreen_2_2()
        {
            PrintTitle();

            PrintList(interestList);

            Console.WriteLine();
            Console.WriteLine("내가 관심 과목으로 추가한 총 학점: " + SumTotalInterestUnit());
            Console.WriteLine();
            Console.Write("삭제하길 원하는 강의의 번호(No.)를 입력하세요: ");
            bool isDeleteDone = false;
            while (!isDeleteDone)
            {
                string input = Console.ReadLine();
                bool isInputValid = false;
                for (int i = 0; i < interestList.Count; i++)
                {
                    if (input == interestList[i].number)
                    {
                        Console.WriteLine("해당 과목이 삭제되었습니다.");
                        for (int j = 0; j < subjectList.Count; j++)
                        {
                            if (interestList[i].number == subjectList[j].number)
                            {
                                subjectList[j].enrollment = "";
                                interestList.RemoveAt(i);
                                break;
                            }
                            else { }
                        }
                        isInputValid = true;
                        isDeleteDone = true;
                        break;
                    }
                    else { }
                }
                if (isInputValid == false)
                {
                    Console.Write("수강 신청 목록 중에 입력하신 번호에 해당하는 과목이 없습니다. 다시 입력하세요: ");
                }
                else { }
            }
            Console.WriteLine("삭제를 계속 하시겠습니까? Y/N");
            bool isDeleteFinished = false;
            while (!isDeleteFinished)
            {
                string answer = Console.ReadLine();
                if (answer == "Y" || answer == "y")
                {
                    PrintScreen_1_2();
                    break;
                }
                else if (answer == "N" || answer == "n")
                {
                    PrintScreen_1();
                    break;
                }
                else
                {
                    Console.WriteLine("다시 입력하세요: ");
                }
            }
        }

        // 2-3 화면: 수강 강의 조회 
        public static void PrintScreen_2_3()
        {
            PrintTitle();

            PrintList(interestList);

            Console.WriteLine();
            Console.WriteLine("내가 수강 신청한 총 학점: " + SumTotalInterestUnit());
            Console.WriteLine("뒤로 돌아가려면 ESC를 누르세요.");
            while (Console.ReadKey().Key != ConsoleKey.Escape) { }
            PrintScreen_1();
        }

        // 2-4 화면: 전체 강의 목록
        public static void PrintScreen_2_4()
        {
            PrintTitle();

            PrintList(subjectList);

            Console.WriteLine();
            Console.WriteLine("뒤로 돌아가려면 ESC를 누르세요.");
            while (Console.ReadKey().Key != ConsoleKey.Escape) { }
            PrintScreen_1();
        }


        // 3번 화면: 나의 시간표 
        public static void PrintScreen_3()
        {
            PrintTitle();

            int column = 0;
            int row = 0;
            Console.SetCursorPosition(30, 10);
            Console.Write("월");
            Console.SetCursorPosition(60, 10);
            Console.Write("화");
            Console.SetCursorPosition(90, 10);
            Console.Write("수");
            Console.SetCursorPosition(120, 10);
            Console.Write("목");
            Console.SetCursorPosition(150, 10);
            Console.Write("금");

            Console.SetCursorPosition(5, 11);
            Console.WriteLine("9:00");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------");

            Console.SetCursorPosition(5, 13);
            Console.WriteLine("9:30");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------");
            Console.SetCursorPosition(5, 15);
            Console.WriteLine("10:00");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------");

            Console.SetCursorPosition(5, 17);
            Console.WriteLine("10:30");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------");
            Console.SetCursorPosition(5, 19);
            Console.WriteLine("11:00");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------");

            Console.SetCursorPosition(5, 21);
            Console.WriteLine("11:30");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------");
            Console.SetCursorPosition(5, 23);
            Console.WriteLine("12:00");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------");

            Console.SetCursorPosition(5, 25);
            Console.WriteLine("12:30");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------");
            Console.SetCursorPosition(5, 27);
            Console.WriteLine("13:00");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------");

            Console.SetCursorPosition(5, 29);
            Console.WriteLine("13:30");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------");
            Console.SetCursorPosition(5, 31);
            Console.WriteLine("14:00");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------");

            Console.SetCursorPosition(5, 33);
            Console.WriteLine("14:30");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------");
            Console.SetCursorPosition(5, 35);
            Console.WriteLine("15:00");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------");

            Console.SetCursorPosition(5, 37);
            Console.WriteLine("15:30");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------");
            Console.SetCursorPosition(5, 39);
            Console.WriteLine("16:00");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------");

            Console.SetCursorPosition(5, 41);
            Console.WriteLine("16:30");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------");
            Console.SetCursorPosition(5, 43);
            Console.WriteLine("17:00");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------");

            Console.SetCursorPosition(5, 45);
            Console.WriteLine("17:30");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------");
            Console.SetCursorPosition(5, 47);
            Console.WriteLine("18:00");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------");

            Console.SetCursorPosition(5, 49);
            Console.WriteLine("18:30");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------");
            Console.SetCursorPosition(5, 51);
            Console.WriteLine("19:00");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------");

            Console.SetCursorPosition(5, 53);
            Console.WriteLine("19:30");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------");

            for (int i=0; i < enrolmentList.Count; i++)
            {
                for (int j=0; j < GetTime(enrolmentList[i]).Count; j += 3)
                {
                    switch (GetTime(enrolmentList[i])[j])
                    {
                        case 0:
                            column = 30;
                            break;
                        case 1:
                            column = 60;
                            break;
                        case 2:
                            column = 90;
                            break;
                        case 3:
                            column = 120;
                            break;
                        case 4:
                            column = 150;
                            break;
                    }

                    double startTime = GetTime(enrolmentList[i])[j + 1];
                    double endTime = GetTime(enrolmentList[i])[j + 2];

                    while (startTime != endTime)
                    {
                        switch (startTime)
                        {
                            case 9:
                                row = 11;
                                Console.SetCursorPosition(column, row);
                                Console.WriteLine(enrolmentList[i].name);
                                break;
                            case 9.5:
                                row = 13;
                                Console.SetCursorPosition(column, row);
                                Console.WriteLine(enrolmentList[i].name);
                                break;
                            case 10:
                                row = 15;
                                Console.SetCursorPosition(column, row);
                                Console.WriteLine(enrolmentList[i].name);
                                break;
                            case 10.5:
                                row = 17;
                                Console.SetCursorPosition(column, row);
                                Console.WriteLine(enrolmentList[i].name);
                                break;
                            case 11:
                                row = 19;
                                Console.SetCursorPosition(column, row);
                                Console.WriteLine(enrolmentList[i].name);
                                break;
                            case 11.5:
                                row = 21;
                                Console.SetCursorPosition(column, row);
                                Console.WriteLine(enrolmentList[i].name);
                                break;
                            case 12:
                                row = 23;
                                Console.SetCursorPosition(column, row);
                                Console.WriteLine(enrolmentList[i].name);
                                break;
                            case 12.5:
                                row = 25;
                                Console.SetCursorPosition(column, row);
                                Console.WriteLine(enrolmentList[i].name);
                                break;
                            case 13:
                                row = 27;
                                Console.SetCursorPosition(column, row);
                                Console.WriteLine(enrolmentList[i].name);
                                break;
                            case 13.5:
                                row = 29;
                                Console.SetCursorPosition(column, row);
                                Console.WriteLine(enrolmentList[i].name);
                                break;
                            case 14:
                                row = 31;
                                Console.SetCursorPosition(column, row);
                                Console.WriteLine(enrolmentList[i].name);
                                break;
                            case 14.5:
                                row = 33;
                                Console.SetCursorPosition(column, row);
                                Console.WriteLine(enrolmentList[i].name);
                                break;
                            case 15:
                                row = 35;
                                Console.SetCursorPosition(column, row);
                                Console.WriteLine(enrolmentList[i].name);
                                break;
                            case 15.5:
                                row = 37;
                                Console.SetCursorPosition(column, row);
                                Console.WriteLine(enrolmentList[i].name);
                                break;
                            case 16:
                                row = 39;
                                Console.SetCursorPosition(column, row);
                                Console.WriteLine(enrolmentList[i].name);
                                break;
                            case 16.5:
                                row = 41;
                                Console.SetCursorPosition(column, row);
                                Console.WriteLine(enrolmentList[i].name);
                                break;
                            case 17:
                                row = 43;
                                Console.SetCursorPosition(column, row);
                                Console.WriteLine(enrolmentList[i].name);
                                break;
                            case 17.5:
                                row = 45;
                                Console.SetCursorPosition(column, row);
                                Console.WriteLine(enrolmentList[i].name);
                                break;
                            case 18:
                                row = 47;
                                Console.SetCursorPosition(column, row);
                                Console.WriteLine(enrolmentList[i].name);
                                break;
                            case 18.5:
                                row = 49;
                                Console.SetCursorPosition(column, row);
                                Console.WriteLine(enrolmentList[i].name);
                                break;
                            case 19:
                                row = 51;
                                Console.SetCursorPosition(column, row);
                                Console.WriteLine(enrolmentList[i].name);
                                break;
                            case 19.5:
                                row = 53;
                                Console.SetCursorPosition(column, row);
                                Console.WriteLine(enrolmentList[i].name);
                                break;
                            default:
                                break;
                        }
                        startTime += 0.5;
                    }

                }
            }

            Console.WriteLine("뒤로 돌아가려면 ESC를 누르세요.");
            while (Console.ReadKey().Key != ConsoleKey.Escape) { }
            PrintMainScreen();
        }

    }
}
