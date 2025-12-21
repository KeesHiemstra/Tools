string[] Months = { "januari", "februari", "maart", "april", "mei", "juni", "juli", "augustus", "september", "oktober", "november", "december" };
string[] Days = { "Zondag", "Maandag", "Dinsdag", "Woensdag", "Donderdag", "Vrijdag", "Zaterdag" };

DateTime startDate = new DateTime(2026, 01, 01);
DateTime endDate = new DateTime(2026, 12, 31);

while (startDate <= endDate)
{
	Console.WriteLine($"{Days[((int)startDate.DayOfWeek)]} {startDate.Day} {Months[startDate.Month - 1]}");
	Console.WriteLine();
	startDate = startDate.AddDays(1);
}