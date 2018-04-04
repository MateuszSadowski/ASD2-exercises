
using System.Collections.Generic;

namespace ASD
{

public interface IList : IEnumerable<int>
    {
    // Je�li element v jest na li�cie to zwraca true
    // Je�li elementu v nie ma na li�cie to zwraca false
    bool Search(int v);

    // Je�li element v jest na li�cie to zwraca false (elementu nie dodaje)
    // Je�li elementu v nie ma na li�cie to dodaje go do listy i zwraca true
    bool Add(int v);

    // Je�li element v jest na li�cie to usuwa go z listy i zwraca true
    // Je�li elementu v nie ma na li�cie to zwraca false
    bool Remove(int v);
    }

//
// dopisa� klas� opisuj�c� element listy
//
    public class Node
    {
        public int val;
        public Node next;

        public Node(int _val, Node _next)
        {
            val = _val;
            next = _next;
        }
    }

// Zwyk�a lista (nie samoorganizuj�ca si�)
public class SimpleList : IList
    {

        // doda� niezb�dne pola
        public Node head;
        public Node tail;

        public SimpleList()
        {
            head = null;
            tail = null;
        }

    // Lista si� nie zmienia
    public bool Search(int v)
        {
            if (null == head)
                return false;

            Node node = head;
            do
            {
                if (node.val == v)
                    return true;
                node = node.next;
            } while (null != node);

        return false;  // zmieni�
        }

    // Element jest dodawany na koniec listy
    public bool Add(int v)
        {
            if (Search(v))
                return false;
            
            if(null == head)
            {
                head = tail = new Node(v, null);
                return true;
            }

            tail.next = new Node(v, null);
            tail = tail.next;

        return true;  // zmieni�
        }

    // Pozosta�e elementy nie zmieniaj� kolejno�ci
        //TODO: fix this
    public bool Remove(int v)
        {
            if (!Search(v))
                return false;
            
            Node node = head;
            Node prev = null;
            do
            {
                if (node.val == v)
                {
                    if (head == tail)   //Only element
                    {
                        head = tail = null;
                        return true;
                    }
                    else if(null == prev)   //First element
                    {
                        head = node.next;
                    }
                    else if(null == node.next)  //Last element
                    {
                        tail = prev;
                        prev.next = null;
                    }
                    else
                    {
                        prev.next = node.next;
                    }

                    return true;
                }

                prev = node;
                node = node.next;
            } while (null != node);
                
        return false;  // zmieni�
        }

    // Wymagane przez interfejs IEnumerable<int>
    public IEnumerator<int> GetEnumerator()
        {
        // nie wolno modyfikowa� kolekcji
            Node node = head;
            while (null != node)
            {
                yield return node.val;
                node = node.next;
            }
            //yield break;
        }

    // Wymagane przez interfejs IEnumerable<int> - nie zmmienia� (jest gotowe!)
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
        return this.GetEnumerator();
        }

    } // class SimpleList


// Lista z przesnoszeniem elementu, do kt�rego by� dost�p na pocz�tek
public class MoveToFrontList : IList
    {

    // doda� niezb�dne pola
        public Node head;
        //public Node tail;

        public MoveToFrontList()
        {
            head = null;
            //tail = null;
        }

    // Znaleziony element jest przenoszony na pocz�tek
    public bool Search(int v)
        {
            if (null == head)
                return false;

            Node node = head;
            Node prev = null;
            do
            {
                if (node.val == v)
                {
                    if (null == prev)
                        return true;
                    else
                    {
                        prev.next = node.next;
                        node.next = head;
                        head = node;
                    }
                    return true;   
                }
                prev = node;
                node = node.next;
            } while (null != node);

        return false;  // zmieni�
        }

    // Element jest dodawany na pocz�tku, a je�li ju� by� na li�cie to jest przenoszony na pocz�tek
    public bool Add(int v)
        {
            if (Search(v))
                return false;
            Node node = new Node(v, head);
            head = node;
        return true;  // zmieni�
        }

    // Pozosta�e elementy nie zmieniaj� kolejno�ci
    public bool Remove(int v)
        {
            Node node = head;
            Node prev = null;
            while (null != node)
            {
                if (node.val == v)
                {
                    if (null == prev)
                        head = node.next;   //FREE HEAD?
                                            //else if (null == node.next)
                                            //prev.next = null;   //FREE NODE?
                    else
                        prev.next = node.next;

                    return true;
                }

                prev = node;
                node = node.next;
            }

            return false;  // zmieni�
        }

    // Wymagane przez interfejs IEnumerable<int>
    public IEnumerator<int> GetEnumerator()
        {
        // nie wolno modyfikowa� kolekcji
            Node node = head;
            while (null != node)
            {
                yield return node.val;
                node = node.next;
            }
        }

    // Wymagane przez interfejs IEnumerable<int> - nie zmmienia� (jest gotowe!)
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
        return this.GetEnumerator();
        }

    } // class MoveToFrontList


} // namespace ASD
