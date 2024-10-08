namespace ConsoleHero;

public static class ListExtensions
{
    public static void PrintInfo(this List<string> lines)
    {
        const int x = 40;
        int y = 0;
        Clear();
        foreach (string line in lines)
        {
            SetCursorPosition(x, y++);
            ForegroundColor = ConsoleColor.White;
            Write(line);
        }
        SetCursorPosition(0, 0);
    }

    public static void PrintInfo(this List<List<ColorInfo>> lines)
    {
        const int x = 40;
        int y = 0;
        const int padding = 2;
        int fieldCount = lines.Max(x => x.Count);
        int[] offsets = new int[fieldCount];

        for (int i = 0; i < fieldCount; i++)
        {
            foreach (List<ColorInfo> line in lines)
            {
                if (i < line.Count && line[i].Text.Length > offsets[i])
                {
                    offsets[i] = line[i].Text.Length;
                }
            }
        }

        for (int i = 0; i < offsets.Length; i++)
        {
            offsets[i] += padding;
        }

        Clear();
        foreach (List<ColorInfo> line in lines)
        {
            int rollingOffset = 0;
            foreach (ColorInfo field in line)
            {
                SetCursorPosition(x + rollingOffset, y);
                rollingOffset += offsets[line.IndexOf(field)];

                ForegroundColor = field.Color;
                Write(field.Text);
            }
            y++;
        }

        ForegroundColor = ConsoleColor.White;
        SetCursorPosition(0, 0);
    }
}