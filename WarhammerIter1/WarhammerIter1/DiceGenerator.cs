///////////////////////////////////////////////////////////
//  DiceGenerator.cs
//  Implementation of the Class DiceGenerator
//  Generated by Enterprise Architect
//  Created on:      04-���-2014 12:03:33
//  Original author: User
///////////////////////////////////////////////////////////

using System.Collections.Generic;
using System;

using Warhammer;
namespace Warhammer
{
    public class TestDice : DiceInt
    {
        List<int> dices;
        int now = -1;
        List<int> ran;
        int ranNow = -1;
        public TestDice(List<int> d, List<int> r)
        {
            dices = d;
            ran = r;
        }

        public int rand(int a, int b)
        {
            ranNow++;
            if (ranNow > dices.Count)
                ranNow = 0;
            return ran[ranNow];
        }

        public int D2()
        {

            return 0;
        }

        public int D3()
        {

            return 0;
        }

        public int D6()
        {
            now++;
            if (now > dices.Count)
                now = 0;
            return dices[now];
        }

        public int D6plD6()
        {
            return D6() + D6();
        }

        public List<int> manyD6(int m)
        {
            List<int> L = new List<int> { };
            for (int i = 0; i < m; i++)
            {
                L.Add(D6());
            }
            return L;
        }

        public int D66()
        {

            return 0;
        }
    }


    public interface DiceInt
    {
        int D2();
        int D3();
        int D6();
        int D6plD6();
        List<int> manyD6(int m);
        int D66();
    }

    public class DiceGenerator : DiceInt
    {
        Random r = new Random();

        public int rand(int a, int b)
        {
            return (r.Next() % (b - a + 1)) + a;
        }

        public int D2()
        {

            return 0;
        }

        public int D3()
        {

            return 0;
        }

        public int D6()
        {

            return r.Next() % 6 + 1;
        }

        public int D6plD6()
        {
            return D6() + D6();
        }

        public List<int> manyD6(int m)
        {
            List<int> L = new List<int> { };
            for (int i = 0; i < m; i++)
            {
                L.Add(D6());
            }
            return L;
        }

        public int D66()
        {

            return 0;
        }

    }//end DiceGenerator
}
