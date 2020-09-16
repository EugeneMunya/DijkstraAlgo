using System;
using System.Collections;
using System.Collections.Generic;

namespace Dijkstra
{
    class Program
    {
        static void Main(string[] args)
        {
         int nodes = 5;
         Graph dijkstra = new Graph(nodes);
         dijkstra.addEdge(0,1,1);
         dijkstra.addEdge(0,2,7);
         dijkstra.addEdge(1,2,5);
         dijkstra.addEdge(1,4,4);
         dijkstra.addEdge(4,3,2);
         dijkstra.addEdge(2,3,6);

         int distance = dijkstra.minmumDistanceBtwTwoNodes(0,3);
        
        Console.WriteLine(distance);
        while(dijkstra.q.Count!=0){
             Console.Write(dijkstra.q.Dequeue());
          
        }
        }   

   
    }

    //Graph
    class Graph{
      List<List<Edge>> graph;
      public bool [] visited;
      int [] distance;
     public Queue q ;

      public Graph(int nodes){
     graph= new List<List<Edge>>();
     visited= new bool[nodes];
     distance = new int[nodes];
     for (int i = 0; i < nodes; i++)
     {
         graph.Insert(i,new List<Edge>());
         distance[i]= int.MaxValue;
     }
      }
      public void addEdge(int sourceNode, int targetNoode, int weight){
        graph[sourceNode].Add(new Edge(targetNoode, weight));
        graph[targetNoode].Add(new Edge(sourceNode,weight));
      }

      public int minmumDistanceBtwTwoNodes(int source, int destination)
      {
        if(source == destination) return 0;
        PriorityQueue<Edge> minHeap = new PriorityQueue<Edge>();
         q = new Queue();
        distance[source]=0;
        minHeap.Enqueue(new Edge(0,0));
        while(minHeap.Count()!=0)
        {
          int v = minHeap.Dequeue().targetNoode;
        
         
          if (visited[v])
          {
              continue;
          }
          
          visited[v]= true;
          q.Enqueue(v);
          List<Edge> childList = graph[v];
          
          foreach (var child in childList)
          {
           
              int weight = child.weight;
              int targetNoode =  child.targetNoode;
              if (!visited[targetNoode] && (distance[v]+weight<distance[targetNoode]))
              {
                 distance[targetNoode]= distance[v]+weight;
                 child.weight= distance[v]+weight;
                 minHeap.Enqueue(child);
              }
          }
        }

        return distance[destination];
      }
    }



// TargetNode with the weight
    class Edge :IComparable<Edge>
    {
     public int targetNoode;
     public int weight;
      public Edge(int target, int weight){
        this.targetNoode= target;
        this.weight= weight;
      }

      public int CompareTo( Edge other)
      {
    if (this.weight  < other.weight) return -1;
    else if (this.weight  > other.weight) return 1;
    else return 0;
      }
      

    }

// PriorityQue implementation
    public class PriorityQueue <T> where T:IComparable<T>
    {
        private List <T> data;

        public PriorityQueue(){
            this.data= new List<T>();
        }

       public int Count()
       {
       return data.Count;
       }
        

        public void Enqueue(T item){
            data.Add(item);
            int ci= data.Count -1;
            while(ci>0){
                int pi = (ci-1)/2;
                if (data[ci].CompareTo(data[pi])>=0)
                {
                    break;
                }
                T temp = data[ci]; 
                data[ci]= data[pi];
                data[pi]= temp;
                ci=pi;
            }
        }

        public T Dequeue()
        {
            int li = data.Count -1;
            T frontItem = data[0];
            data[0]= data[li];
            data.RemoveAt(li);
            --li;
            int pi=0;
            while (true)
            {
               int ci = (2*pi)+1;
               if (ci>li)
               {
                 break;  
               } 
               int rc= ci+1;
               if (rc<=li && data[rc].CompareTo(data[ci])<0)
               {
                  ci=rc; 
               }
               if (data[pi].CompareTo(data[ci])<=0)
               {
                 break;  
               }
               T temp = data[pi];
               data[pi]=data[ci];
               data[ci]=temp;
               pi=ci;
            }
         return frontItem;

        }

    }

}
