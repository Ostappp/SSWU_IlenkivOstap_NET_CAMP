using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Homework4
{
    internal class Task3
    {

        private string[] _data;
        private FlatData[] _flats;
        private int _quarterNumber;
        private int _minYear = int.MaxValue;
        private CultureInfo culture;
        public Task3(string data)
        {
            _data = data.Split('\n');
            SortData(_data);
        }
        public Task3(IEnumerable<string> data)
        {
            _data = data.ToArray();
            SortData(_data);
        }
        private void SortData(string[] data)
        {
            List<FlatData> flatData = new List<FlatData>();
            _quarterNumber = int.Parse(data[0].Split(' ')[1]);
            if (_quarterNumber < 1 || _quarterNumber > 4)
                throw new ArgumentException("quarter must bi grater or equal than 1 and less or equal than 4");

            List<string> stringData = new List<string>();
            foreach (var lineData in data[1..])
            {
                int fNum = int.Parse(lineData.Split(' ')[0]);
                string fAddr = lineData.Split(' ')[1];
                object[] obj = GetStringData(lineData);

                if (!flatData.Where(f => f.flatNum == fNum && f.address == fAddr).Any())
                    flatData.Add(new FlatData(obj));
                else
                {
                    int flatIndex = flatData.FindIndex(f => f.flatNum == fNum && f.address == fAddr);
                    flatData[flatIndex].AddRecord(obj.Last());
                }
            }
            _flats = new FlatData[flatData.Count];
            _flats = flatData.OrderBy(o => o.address).ThenBy(o => o.flatNum).ToArray();
        }
        private object[] GetStringData(string data)
        {
            string[] strings = data.Split(' ');
            int flatNum = int.Parse(strings[0]);
            string address = strings[1];
            string ownerSurname = strings[2];
            //values of each month
            float inputDisplays = float.Parse(strings[3].Replace('.', ','));
            float outputDisplays = float.Parse(strings[4].Replace('.', ','));
            culture = CultureInfo.GetCultureInfo("uk-UA");
            DateOnly dateOfReading = DateOnly.Parse(strings[5], culture);

            if (dateOfReading.Year < _minYear)
                _minYear = dateOfReading.Year;

            KeyValuePair<DateOnly, (float, float)> recordData = new KeyValuePair<DateOnly, (float, float)>(dateOfReading, (inputDisplays, outputDisplays));
            return new object[] { flatNum, address, ownerSurname, recordData };
        }
        public IEnumerable<string> GetTotalReport(string address = "")
        {
            if (address.Length == 0)
                address = "всіма адресами";

            int startMonth = 3 * _quarterNumber - 2;
            int endMonth = 3 * _quarterNumber;
            List<string> result = new List<string> { $"Загальний звіт по споживанню електроенергії за {_quarterNumber} квартал ({new DateOnly(2023, startMonth, 14):MMMM} - {new DateOnly(2023, endMonth, 14):MMMM}) за адресою: {address}" };

            for (int i = DateTime.Now.Year; i >= _minYear; i--)
            {
                string report = string.Empty;
                for (int j = endMonth; j >= startMonth; j--)
                {
                    report = $"\n{new DateOnly(i, j, 14):MMMM} {i} року:";
                    foreach (FlatData flat in address == "всіма адресами" ? _flats : _flats.Where(f => f.address == address).Select(f => f))
                    {

                        if (flat.records.Any(d => d.Key.Year == i && d.Key.Month == j))
                        {
                            flat.SortRecordsByDesc();
                            var record = flat.records.First(d => d.Key.Year == i && d.Key.Month == j);
                            report += String.Format("\n\tНомер квартири: {0,-3} Власник: {1,-32} Вхідний показ: {2,-13} Вихідний показ: {3,-13} Дата подачі: {4}.",
                                        flat.flatNum, flat.ownerSurname, record.Value.Item1, record.Value.Item2, record.Key);
                        }
                    }
                    result.Add(report);
                }

            }
            return result;
        }
        public string GetReportByApartment(int flatNum, string address = "")
        {
            if (address.Length == 0)
                address = _flats[0].address;

            FlatData flat = _flats.Where(f => f.flatNum == flatNum && f.address == address).Select(x => x).FirstOrDefault();
            string owner = (flat.ownerSurname == null) ? "Невідомий" : flat.ownerSurname;
            List<string> report = new List<string> { $"Номер квартири: {flatNum,-3}\tАдреса: {address,-64}\tВласник: {owner}\n" };

            if (flat.flatNum == 0)
                return string.Join("\n\t\t", report[0], $"Квартиру не знайдено");



            flat.SortRecordsByDesc();
            foreach (var record in flat.records)
            {
                report.Add(String.Format("Вхідний показ: {0,-13} Вихідний показ: {1,-13} Дата подачі: {2}.",
                            record.Value.Item1, record.Value.Item2, record.Key));
            }
            return string.Join("\n\t\t", report);
        }

        //вважаємо що ніхто нічого не оплачував, отримуємо найбільше значення у заданому кварталі певного року (за замовчуванням 2023)
        public string GetSurnameWithHighestArrears(double pricePerWatt, int year = 2023, string address = "")
        {
            if (address.Length == 0)
                address = "всіма адресами";

            double maxPrice = 0;
            FlatData seekedFlat = new FlatData();
            foreach (var flat in address == "всіма адресами" ? _flats : _flats.Where(f => f.address == address).Select(f => f))
            {
                if (flat.records.Any(d => d.Key.Year == year && d.Key.Month == _quarterNumber * 3))
                {
                    flat.SortRecordsByDesc();
                    double price = (flat.records.First(d => d.Key.Year == year && d.Key.Month == _quarterNumber * 3).Value.Item2
                                            - flat.records.Last(d => d.Key.Year == year && d.Key.Month == _quarterNumber * 3 - 2).Value.Item1) * pricePerWatt;
                    if (price > maxPrice)
                    {
                        seekedFlat = flat;
                        maxPrice = price;
                    }
                }

            }
            return seekedFlat.ownerSurname; 
        }
        //повертаємо номер квартири яка не використоваувала електрику протягом кварталу обраного року
        public List<int> GetAppartmentNumWithoutElectricityUse(int year = 2023, string address = "")
        {
            List<int> flatNums = new List<int>();
            foreach (var flat in address.Length == 0 ? _flats : _flats.Where(f => f.address == address).Select(f => f))
            {
                if (flat.records.Any(d => d.Key.Year == year && d.Key.Month == _quarterNumber * 3))
                {
                    flat.SortRecordsByDesc();
                    if (flat.records.First(d => d.Key.Year == year && d.Key.Month == _quarterNumber * 3).Value.Item2
                        - flat.records.Last(d => d.Key.Year == year && d.Key.Month == _quarterNumber * 3 - 2).Value.Item1 == 0)
                        flatNums.Add(flat.flatNum);
                }
            }
            return flatNums;
        }
        //повертаємо запис витрат на електроенергію протягом кварталу обраного року
        public List<string> TotalSpending(double pricePerWatt, int year = 2023)
        {
            List<string> report = new List<string>
            {
                $"Звітність за {_quarterNumber} квартал {year} року:"
            };
            foreach (var flat in _flats)
            {
                flat.SortRecordsByDesc();
                float input = flat.records.Last(d => d.Key.Year == year && d.Key.Month == _quarterNumber * 3 - 2).Value.Item1;
                float output = flat.records.First(d => d.Key.Year == year && d.Key.Month == _quarterNumber * 3).Value.Item2;
                decimal price = (decimal)((output - input) * pricePerWatt);

                report.Add(string.Format("\t\tНомер квартири: {0,-3} Адреса: {1,-64} Власник: {2,-16} Початковий показник: {3,-13} Кінцевий показник: {4,-13} До оплати: {5}",
                    flat.flatNum, flat.address, flat.ownerSurname, input, output, price.ToString("c", culture)));
            }
            return report;
        }
        public List<string> TimeFromLastMeterReading()
        {
            List<string> report = new List<string> { "Кількість днів з моменту останнього зняття показу лічитльника:" };
            foreach (var flat in _flats)
            {
                flat.SortRecordsByDesc();


                report.Add(string.Format("\t\tНомер квартири: {0,-3}        Адреса: {1,-64} Власник: {2,-16} З останньої подачі показників минуло {3} дні.",
                    flat.flatNum, flat.address, flat.ownerSurname, DateOnly.FromDateTime(DateTime.Now).DayNumber - flat.records.First().Key.DayNumber));
            }
            return report;
        }


        struct FlatData
        {
            public int flatNum;
            public string address;
            public string ownerSurname;
            public Dictionary<DateOnly, (float, float)> records;
            public FlatData(object[] data)
            {
                records = new Dictionary<DateOnly, (float, float)>();

                flatNum = (int)data[0];
                address = (string)data[1];
                ownerSurname = (string)data[2];

                KeyValuePair<DateOnly, (float, float)> recordData = (KeyValuePair<DateOnly, (float, float)>)data[3];
                records.Add(recordData.Key, recordData.Value);
            }
            public void AddRecord(KeyValuePair<DateOnly, (float, float)> recordData)
            {
                records.Add(recordData.Key, recordData.Value);
            }
            public void AddRecord(object data)
            {
                KeyValuePair<DateOnly, (float, float)> recordData = (KeyValuePair<DateOnly, (float, float)>)data;
                records.Add(recordData.Key, recordData.Value);
            }
            public void SortRecordsByDesc() => records = records.OrderByDescending(d => d.Key.Year).ThenByDescending(d => d.Key.Month).ThenByDescending(d => d.Key.Day).ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
