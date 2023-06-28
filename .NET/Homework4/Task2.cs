namespace Homework4
{
    internal class Task2
    {
        private string _text;
        const int LOCAL_PART_LENGHT_LIMIT = 64;
        const int DOMAIN_PART_LENGHT_LIMIT = 255;
        const int DOMAIN_SEGEMNT_LENGHT_LIMIT = 63;
        public Task2(string text)
        {
            _text = text;
        }
        public List<string> GetCorrectMails(out List<string> incorrectAddress)
        {
            //розділимо слова пробілами і виберемо ті, що містять бодай одну '@'
            List<string> text = _text.Split(new string[] { "\r", "\r\n", "\n", "\t", "\0" }, StringSplitOptions.TrimEntries /*StringSplitOptions.RemoveEmptyEntries*/).ToList();
            
            //possibleAddress = possibleAddress.Where(s => s.Contains('@')).ToList();
            List<string> possibleAddress = new List<string>();
            
            //виловлюємо всі можливі варіанти
            foreach (string line in text)
            {
                if (line.Contains('@'))
                {
                    int i = line.IndexOf('@');
                    int indexStart = 0;
                    bool findAddresEnd = false;
                    while (i < line.Length && i != -1)
                    {
                        if (!findAddresEnd)//шукаємо початок адлреси
                        {
                            if (i > 0)//локальна частина складається із лапок
                            {
                                if (line[i - 1] == '"')
                                {
                                    indexStart = line.LastIndexOf('"', i - 2);
                                }
                                else
                                {
                                    indexStart = line.LastIndexOf(' ', i - 1);
                                }
                                if (indexStart < 0)
                                    indexStart = 0;
                                findAddresEnd = true;
                            }


                        }
                        if (findAddresEnd)//шукаємо кінець адреси
                        {
                            if (i == line.Length - 1 || line[i + 1] == ' ')
                            {
                                findAddresEnd = false;
                                string s = line[indexStart..(i + 1)].Trim();
                                possibleAddress.Add(line[indexStart..(i + 1)].Trim());
                                i = i + 1 == line.Length ? -1 : line.IndexOf('@', i + 1);
                                continue;
                            }
                        }

                        i++;
                    }
                }
            }
            
            //повернемо список дійсних адрес через return, не дійсних через параметр out
            return GetCorrectAddress(possibleAddress, out incorrectAddress);
        }
        public List<string> GetCorrectAddress(List<string> possibleAddress, out List<string> incorrectAddress)
        {
            List<string> correctAddress = new List<string>();
            incorrectAddress = new List<string>();
            foreach (string address in possibleAddress)
            {
                if (IsLocalPartCorrect(address) && IsDomainCorrect(address))
                    correctAddress.Add(address);
                else
                    incorrectAddress.Add(address);
            }
            return correctAddress;
        }
        /// <summary>
        /// локальна частина може бути довжиною до 64 октетів (байтів).
        /// char займає 2 байти, тобто довжина = 32 символи.
        /// 
        /// Локальна частина електронної адреси може бути без лапок або може бути включена у лапки.
        /// У випадку використання лапок, адреса може містити пробіл, горизонтальну вкладку (HT), будь-яку графіку ASCII, крім зворотної косої і лапок або будь-якої графіки ASCII.
        /// якщо лапок немаю діють такі правла: 
        /// 1) великі і малі латинські літери від A до Z і від a до z. 
        /// 2) цифри від 0 до 9
        /// 3) друковані символи ! #$%&'*+-/=? ^_`{|}~
        /// 4) крапка ., за умови, що це не перший чи останній символ та за умови, що він не з'являється послідовно 
        /// </summary>
        bool IsLocalPartCorrect(string mail)
        {
            //не може містит більше одного символу @
            if (mail.ToArray().Where(m => m == '@').Count() > 1 || !mail.Contains('@'))
                return false;

            string localPart = mail.Split('@')[0];

            //локальна частина не може бути пустою
            if(localPart.Length == 0) 
                return false;

            //якщо правильно зрозумів - то все що знаходиться в коментарі ігнорується
            if (!CheckCoomentAndAddrWithoutComment(ref localPart))
                return false;

            //довжина не більше 64 байтів (32 char символи)
            if (localPart.Length > LOCAL_PART_LENGHT_LIMIT)
                return false;
            //якщо правильно зрозумів, то усі символи повинні належати ASCII
            foreach (char c in localPart)
            {
                if (!char.IsAscii(c))
                    return false;
            }



            if (localPart.StartsWith('"'))
            {
                if (!localPart.EndsWith('"'))
                    return false;

                //не може містити зворотні косі
                if (localPart.Contains('\\'))
                    return false;

                //не може містити лапки в середині
                if (localPart.Where(p => p == '"').Count() > 2)
                    return false;

                //символи повинні бути лише ASCII
                foreach (char c in localPart)
                {
                    if (!char.IsAscii(c))
                        return false;
                }


                return true;
            }
            else
            {
                if (localPart.Contains('.'))
                {
                    //крапка не може бути першою чи останньою
                    if (localPart[0] == '.')
                        return false;
                    if (localPart.Last() == '.')
                        return false;

                    //не може містити крапки підрят
                    if (localPart.Where(x => x == '.').Count() > 1)
                    {
                        for (int i = 1; i < localPart.Length - 1; i++)
                        {
                            if (localPart[i] == '.')
                                if (localPart[i] == localPart[i + 1])
                                    return false;
                        }
                    }
                }
                string allowedChars = "! #$%&'*+-/=? ^_`{|}~.";//крапка додана до списку, бо правильність її положення перевірина
                foreach (char c in localPart)
                {

                    if (!char.IsAsciiLetterOrDigit(c) && !allowedChars.Contains(c))
                        return false;
                }
                return true;
            }
        }
        /// <summary>
        ///вона повинна відповідати вимогам до імені хоста, списку розділених крапками міток DNS,
        ///кожна мітка має обмеження довжиною 63 символи і складається з:
        ///     великі і малі латинські літери від A до Z і a до z;
        ///     цифри від 0 до 9, за умови, що доменні імена верхнього рівня не є цілими;
        ///     дефіс - за умови, що це не перший чи останній символ.
        /// </summary>
        bool IsDomainCorrect(string mail)
        {
            //не може містит більше одного символу @
            if (mail.ToArray().Where(m => m == '@').Count() > 1 || !mail.Contains('@'))
                return false;

            string domainPart = mail.Split('@')[1];
            
            //доменна частина не може бути пустою
            if (domainPart.Length == 0)
                return false;

            //якщо правильно зрозумів - то все що знаходиться в коментарі ігнорується
            if (!CheckCoomentAndAddrWithoutComment(ref domainPart))
                return false;
            //довжина не більше 255 байтів (127 char символи)
            if (domainPart.Length > DOMAIN_PART_LENGHT_LIMIT)
                return false;



            //якщо правильно зрозумів, то усі символи повинні належати ASCII
            foreach (char c in domainPart)
            {
                if (!char.IsAscii(c))
                    return false;
            }
            if (domainPart.StartsWith('.') || domainPart.EndsWith('.'))
                return false;

            //розділимо доменну частину на сегменти, що роділяються крапкою
            List<string> domainParts = domainPart.Split('.').ToList();
            foreach (string part in domainParts)
            {

                if (part.Length > DOMAIN_SEGEMNT_LENGHT_LIMIT)
                    return false;
                if (part.StartsWith("-") || part.EndsWith("-"))
                    return false;

                foreach (char c in part)
                {
                    if (!(char.IsLetterOrDigit(c) || c == '-'))
                        return false;
                }
            }


            return true;
        }
        bool CheckCoomentAndAddrWithoutComment(ref string addressPart)
        {
            if (!addressPart.Contains('('))
                return true;

            bool isComment = false;
            List<(int, int)> strtEndIndexComment = new List<(int, int)>();

            //перевіряємо чи коментар не має вкладень і знаходимо усі коментарі
            for (int i = 0; i < addressPart.Length; i++)
            {
                if (addressPart[i] == '(' && isComment)
                    return false;

                else if (addressPart[i] == '(' && !isComment)
                {
                    isComment = true;
                    strtEndIndexComment.Add((i, 0));
                }

                else if (addressPart[i] == ')' && isComment)
                {
                    isComment = false;
                    strtEndIndexComment.Insert(strtEndIndexComment.Count - 1, (strtEndIndexComment.Last().Item1, i));
                }
            }
            string clearAddress = string.Empty;
            int commentIndex = 0;
            for (int i = 0; i < addressPart.Length; i++)
            {
                //перевіряємо чи дійшли до кінця поточного коментаря
                if (i == strtEndIndexComment[commentIndex].Item2 + 1 && commentIndex < strtEndIndexComment.Count - 1)
                    commentIndex++;

                if (!(i >= strtEndIndexComment[commentIndex].Item1 && i <= strtEndIndexComment[commentIndex].Item2))
                    clearAddress += addressPart[i];
            }
            addressPart = clearAddress;
            return true;
        }
    }
}
