﻿

internal class Heart : Figure
{
    private int a = 0;

    public int A
    {
        get
        {
            return a;
        }

        set
        {
            a = value;
        }
    }

    public Heart(int a)
    {
        A = a;
    }
}