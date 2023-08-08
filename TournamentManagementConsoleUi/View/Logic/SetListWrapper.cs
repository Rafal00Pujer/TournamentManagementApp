using System.Collections;
using Terminal.Gui;
using TournamentManagementConsoleUi.Logic.Model;

namespace TournamentManagementConsoleUi.View.Logic;

public class SetListWrapper : IListDataSource
{
    private readonly List<SetModel> _sets;
    private BitArray _marks;

    public int Length { get; }

    public int Count => _sets.Count;

    public SetListWrapper(List<SetModel> sets, int length = 40)
    {
        _sets = sets;
        Length = length;

        _marks = new BitArray(_sets.Count);
    }

    public void Render(ListView container, ConsoleDriver driver, bool selected, int item, int col, int line, int width,
        int start = 0)
    {
        container.Move(col, line);

        var set = _sets[item];

        var firstScore = (set.FirstTeamScore.ToString() + " ").PadLeft(width / 2);
        var secondScore = " " + set.SecondTeamScore.ToString().PadRight(width / 2);

        var str = firstScore + secondScore;

        driver.AddStr(str);
    }

    public bool IsMarked(int item)
    {
        throw new NotImplementedException();

        if (item < 0)
        {
            return false;
        }

        if (item > _sets.Count)
        {
            return false;
        }

        if (_sets.Count > _marks.Count)
        {
            var oldMarks = _marks;
            _marks = new BitArray(_sets.Count);

            for (var i = 0; i < oldMarks.Length; i++)
            {
                _marks[i] = oldMarks[i];
            }
        }

        return _marks[item];
    }

    public void SetMark(int item, bool value)
    {
        throw new NotImplementedException();

        if (item < 0)
        {
            return;
        }

        if (item > _sets.Count)
        {
            return;
        }

        if (_sets.Count > _marks.Count)
        {
            var oldMarks = _marks;
            _marks = new BitArray(_sets.Count);

            for (var i = 0; i < oldMarks.Length; i++)
            {
                _marks[i] = oldMarks[i];
            }
        }
        _marks[item] = value;
    }

    public IList ToList()
    {
        return _sets;
    }
}

