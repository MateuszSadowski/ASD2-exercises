
using System;
using System.Collections.Generic;
using ASD.Graphs;

namespace ASD
{

public static class Lab03GraphExtender
    {

    /// <summary>
    /// Wyszukiwanie cykli w grafie
    /// </summary>
    /// <param name="g">Badany graf</param>
    /// <param name="cycle">Znaleziony cykl</param>
    /// <returns>Informacja czy graf jest acykliczny</returns>
    /// <remarks>
    /// 1) Algorytm powinien dzia�a� zar�wno dla graf�w skierowanych, jak i nieskierowanych
    /// 2) Grafu nie wolno zmienia�
    /// 3) Je�li graf zawiera cykl to parametr cycle powinien by� tablic� kraw�dzi tworz�cych dowolny z cykli.
    ///    Kraw�dzie musz� by� umieszczone we w�a�ciwej kolejno�ci (tak jak w cyklu, mo�na rozpocz�� od dowolnej)
    /// 4) Je�li w grafie nie ma cyklu to parametr cycle ma warto�� null.
    /// </remarks>
    public static bool FindCycle(this Graph g, out Edge[] cycle)
        {
        cycle=null;
        return true;
        }

    /// <summary>
    /// Wyznaczanie centrum drzewa
    /// </summary>
    /// <param name="g">Badany graf</param>
    /// <param name="center">Znalezione centrum</param>
    /// <returns>Informacja czy badany graf jest drzewem</returns>
    /// <remarks>
    /// 1) Dla graf�w skierowanych metoda powinna zg�asza� wyj�tek ArgumentException
    /// 2) Grafu nie wolno zmienia�
    /// 3) Parametr center to 1-elementowa lub 2-elementowa tablica zawieraj�ca numery wierzcho�k�w stanowi�cych centrum.
    ///    (w przypadku 2 wierzcho�k�w ich kolejno�� jest dowolna)
    /// </remarks>
    public static bool TreeCenter(this Graph g, out int[] center)
        {
        center = null;
        return true;
        }

    }

}
