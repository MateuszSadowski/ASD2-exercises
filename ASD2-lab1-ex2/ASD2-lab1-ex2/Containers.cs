
using System;

namespace ASD
{

public interface IContainer
    {
    void Put(int x);      //  dodaje element do kontenera

    int  Get();           //  zwraca pierwszy element kontenera i usuwa go z kontenera
                          //  w przypadku pustego kontenera zg�asza wyj�tek typu EmptyException (zdefiniowany w Lab01_Main.cs)

    int  Peek();          //  zwraca pierwszy element kontenera (ten, kt�ry b�dzie pobrany jako pierwszy),
                          //  ale pozostawia go w kontenerze (czyli nie zmienia zawarto�ci kontenera)
                          //  w przypadku pustego kontenera zg�asza wyj�tek typu EmptyException (zdefiniowany w Lab01_Main.cs)

    int  Count { get; }   //  zwraca liczb� element�w w kontenerze

    int  Size  { get; }   //  zwraca rozmiar kontenera (rozmiar wewn�tznej tablicy)
    }

public class Stack : IContainer
    {
    private int[] tab;      // wewn�trzna tablica do pami�tania element�w
    private int count = 0;  // liczba element�w kontenera - metody Put i Get powinny (musz�) to aktualizowa�
    // nie wolno dodawa� �adnych p�l ani innych sk�adowych

    public Stack(int n=2)
        {
        tab = new int[n>2?n:2];
        }

    public void Put(int x)
        {
            // uzupe�ni�
            if (count == Size)
                Array.Resize(ref tab, 2 * Size);
            tab[count++] = x;
        }

    public int Get()
        {
            if (0 == count)
                throw new EmptyException();
            int x = tab[--count];
        return x; // zmieni�
        }

    public int Peek()
        {
            if (0 == count)
                throw new EmptyException();
            int x = tab[count - 1];
            return x; // zmieni�
        }

    public int Count => count;

    public int Size => tab.Length;

    } // class Stack


public class Queue : IContainer
    {
    private int[] tab;      // wewn�trzna tablica do pami�tania element�w
    private int count = 0;  // liczba element�w kontenera - metody Put i Get powinny (musz�) to aktualizowa�
                            // mo�na doda� jedno pole (wi�cej nie potrzeba)
        private int ind = -1;//Points to the last put element

    public Queue(int n=2)
        {
        tab = new int[n>2?n:2];
        }

    public void Put(int x)
        {
            if (count == Size)
            {
                if(ind + 1 == count)
                {
                    Array.Resize(ref tab, 2 * Size);
                    tab[++ind] = x;
                    count++;
                    return;
                }
                else
                {
                    int[] tmp = new int[2 * Size];
                    for (int i = 0; i < count; i++)
                    {
                        tmp[i] = tab[++ind];
                        if (ind + 1 == count)
                            ind = -1;
                    }
                    tmp[++ind] = x;
                    count++;
                    tab = tmp;
                    return;
                }
            }
            if (ind + 1 == Size)
                ind = -1;
            tab[++ind] = x;
            count++;
            
        // uzupe�ni�
        }

    public int Get()
        {
            if (0 == count)
                throw new EmptyException();
            int x;
            if(ind + 1 >= count)
            {
                x = tab[ind + 1 - count];
                count--;
            }
            else
            {
                x = tab[ind + 1 - count + Size];
                count--;
            }
        return x; // zmieni�
        }

    public int Peek()
        {
            if (0 == count)
                throw new EmptyException();
            int x;
            if (ind + 1 >= count)
            {
                x = tab[ind + 1 - count];
            }
            else
            {
                x = tab[ind + 1 - count + Size];
            }
            return x; // zmieni�
        }

    public int Count => count;

    public int Size => tab.Length;

    } // class Queue


public class LazyPriorityQueue : IContainer
    {
    private int[] tab;      // wewn�trzna tablica do pami�tania element�w
    private int count = 0;  // liczba element�w kontenera - metody Put i Get powinny (musz�) to aktualizowa�
    // nie wolno dodawa� �adnych p�l ani innych sk�adowych

    public LazyPriorityQueue(int n=2)
        {
        tab = new int[n>2?n:2];
        }

    public void Put(int x)
        {
        // uzupe�ni�
        }

    public int Get()
        {
        return 0; // zmieni�
        }

    public int Peek()
        {
        return 0; // zmieni�
        }

    public int Count => count;

    public int Size => tab.Length;

    } // class LazyPriorityQueue


public class HeapPriorityQueue : IContainer
    {
    private int[] tab;      // wewn�trzna tablica do pami�tania element�w
    private int count = 0;  // liczba element�w kontenera - metody Put i Get powinny (musz�) to aktualizowa�
    // nie wolno dodawa� �adnych p�l ani innych sk�adowych

    public HeapPriorityQueue(int n=2)
        {
        tab = new int[n>2?n:2];
        }

    public void Put(int x)
        {
        // uzupe�ni�
        }

    public int Get()
        {
        return 0; // zmieni�
        }

    public int Peek()
        {
        return 0; // zmieni�
        }

    public int Count => count;

    public int Size => tab.Length;

    } // class HeapPriorityQueue

} // namespace ASD
