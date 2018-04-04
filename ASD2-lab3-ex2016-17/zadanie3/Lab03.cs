
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
    /// 1) Algorytm powinien dzia³aæ zarówno dla grafów skierowanych, jak i nieskierowanych
    /// 2) Grafu nie wolno zmieniaæ
    /// 3) Jeœli graf zawiera cykl to parametr cycle powinien byæ tablic¹ krawêdzi tworz¹cych dowolny z cykli.
    ///    Krawêdzie musz¹ byæ umieszczone we w³aœciwej kolejnoœci (tak jak w cyklu, mo¿na rozpocz¹æ od dowolnej)
    /// 4) Jeœli w grafie nie ma cyklu to parametr cycle ma wartoœæ null.
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
    /// 1) Dla grafów skierowanych metoda powinna zg³aszaæ wyj¹tek ArgumentException
    /// 2) Grafu nie wolno zmieniaæ
    /// 3) Parametr center to 1-elementowa lub 2-elementowa tablica zawieraj¹ca numery wierzcho³ków stanowi¹cych centrum.
    ///    (w przypadku 2 wierzcho³ków ich kolejnoœæ jest dowolna)
    /// </remarks>
    public static bool TreeCenter(this Graph g, out int[] center)
        {
        center = null;
        return true;
        }

    }

}
