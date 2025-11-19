using ClosedXML.Excel;

namespace Excel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dict = new Dictionary<int, int>();
            for (int i = 1; i <= 49; i++)
            {
                dict.Add(i, 0);
            }

            var workbook = new XLWorkbook("game_results.xlsx");
            var worksheet = workbook.Worksheets.First();
            var columns = worksheet.ColumnsUsed().Select(x => x.ColumnLetter()).ToList();
            var columns2 = worksheet.Row(1).CellsUsed().Select(x => x.Value).ToList();
            var cellValue = worksheet.Cell(3, 4).Value.ToString();

            for (int i = 2; i <= worksheet.RowsUsed().Count(); i++)
            {
                var row = worksheet.Row(i);
                for (int j = 3; j <= 8; j++)
                {
                    var val = Convert.ToInt32(row.Cell(j).Value.ToString());
                    var dictEl = dict.First(x => x.Key == val);
                    dict[val] = dictEl.Value + 1;
                }
            }

            var res = dict.OrderBy(x => x.Value);
            using (var file = new StreamWriter("result.txt"))
            {
                foreach (var keyValuePair in res)
                {
                    file.WriteLine($"{keyValuePair.Key.ToString().PadRight(3)} - {keyValuePair.Value}");
                }

                file.Close();
            }
        }
    }
}
