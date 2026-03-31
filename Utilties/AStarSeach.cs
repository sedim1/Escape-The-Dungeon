using System.Numerics;
using DungeonCrawlerJam2026.GameComponents;
using Raylib_cs;

namespace DungeonCrawlerJam2026.Utilties;

public static class AStarSeach
{
    public struct Node
    {
        public Vector2i parent;
        public double f, g, h;
    }

    public static void DebugPath(GameMap map, List<Character> characters, Character src, Character dest)
    {
        Stack<Vector2i> path = AStar(map, characters, src, dest);
        if (path == null)
        {
            Console.WriteLine("ASTAR ERROR: Path has not been found");
            return;
        }

        while (path.Count > 0)
        {
            Vector2i position = path.Peek();
            path.Pop();
            Vector2 worldPosition = position.CellToWorld();
            Raylib.DrawCircleV(worldPosition,Global.GRIDSCALE/2/2/2,Color.Yellow);
        }
    }
    
    public static Stack<Vector2i> AStar(GameMap map,List<Character> characters,Character src, Character dest)
    {
        Vector2i startPos = src.cellPosition;
        Vector2i endPos = dest.cellPosition;
        //Check if dest position is a valid position
        if (startPos.Equals(endPos))
            return null;
        if (MovementSystem.OutOfBounds(map, startPos) || MovementSystem.OutOfBounds(map, endPos))
            return null;
        if (!map.positionIsFloor(startPos.X, startPos.Y) || !map.positionIsFloor(endPos.X, endPos.Y))
            return null;
        //Initialize open and close list
        bool[,] closedList = new bool[map.GetHeight(), map.GetWidth()];
        Node[,] cellDetails = new Node[map.GetHeight(), map.GetWidth()];
        for (int i = 0; i < map.GetHeight(); i++)
        {
            for (int j = 0; j < map.GetWidth(); j++)
            {
                cellDetails[i, j].f = double.MaxValue;
                cellDetails[i, j].g = double.MaxValue;
                cellDetails[i, j].h = double.MaxValue;
                cellDetails[i, j].parent = new Vector2i(-1, -1);
            }
        }
        //Start parameters of starting node
        int x = startPos.X; int y =  startPos.Y;
        cellDetails[y, x].parent = startPos;
        cellDetails[y, x].g = 0.0;
        cellDetails[y, x].f = 0.0;
        cellDetails[y, x].h = 0.0;
        
        SortedSet<(double, Vector2i)> openList = new SortedSet<(double, Vector2i)>(
            Comparer<(double, Vector2i)>.Create((a, b) =>
                {
                    int cmp = a.Item1.CompareTo(b.Item1);
                    if (cmp != 0) return cmp;
                    int cx = a.Item2.X.CompareTo(b.Item2.X);
                    if (cx != 0) return cx;
                    return a.Item2.Y.CompareTo(b.Item2.Y);
                }
                ));
        
        openList.Add((0.0f, new Vector2i(x, y)));

        bool foundDest = false;

        Vector2i[] directions =
        {
            new Vector2i(1,0),
            new Vector2i(0,1),
            new Vector2i(-1,0),
            new Vector2i(0,-1)
        };
        
        //Console.WriteLine("Starting search from: " + startPos.ToString()+" to " + endPos.ToString());
        while (openList.Count > 0)
        {
            (double f, Vector2i pair) p = openList.Min;
            openList.Remove(p);
            //Add this vertex to the closed list
            x = p.pair.X;
            y = p.pair.Y;
            Vector2i current = new Vector2i(x, y);
            //Console.WriteLine("Current cell: " + current.ToString());
            closedList[y, x] = true;
            //Generate successors for current cell
            for (int k = 0; k < directions.Length; k++)
            {
                int newX = x + directions[k].X;
                int newY = y + directions[k].Y;
                Vector2i succesor = new Vector2i(newX, newY);
                //Console.WriteLine("Next position: " + succesor.ToString());
                //End if destination has been reached
                if (succesor.Equals(endPos))
                {
                    cellDetails[succesor.Y, succesor.X].parent = current;
                    //Console.WriteLine("Destination has been reached");
                    return TracePath(cellDetails, endPos);
                }
                //Ignore iteration if next position is not valid
                if (MovementSystem.OutOfBounds(map, succesor))
                {
                    //Console.WriteLine("Next position is out of bounds");
                    continue;
                }
                if (!map.positionIsFloor(succesor.X, succesor.Y))
                {
                    //Console.WriteLine("Next position is blocked by a wall");
                    continue;
                }
                if (positionIsOccupied(characters, src, dest, succesor))
                {
                    //Console.WriteLine("Next position is occupied by a character");
                    continue;
                }
                if (closedList[succesor.Y, succesor.X])
                {
                    //Console.WriteLine("Next position is already on closed list");
                    continue;
                }

                double newG = cellDetails[current.Y, current.X].g + 1.0f;
                double newH = calculateHValue(succesor, endPos);
                double newF = newG + newH;
                if ( cellDetails[newY,newX].f ==  double.MaxValue || cellDetails[newY, newX].f > newF)
                {
                    //Console.WriteLine("Cell added");
                    openList.Add((newF, succesor));
                    //Update details of this cell
                    cellDetails[succesor.Y, succesor.X].f = newF;
                    cellDetails[succesor.Y, succesor.X].g = newG;
                    cellDetails[succesor.Y, succesor.X].h = newH;
                    cellDetails[succesor.Y, succesor.X].parent = current;
                }
            }
        }
        
        return null;
    }
    
    static double calculateHValue(Vector2i current, Vector2i dest)
    {
        return ( Math.Abs(current.X - dest.X) + Math.Abs(current.Y - dest.Y) );
    }

    static bool positionIsOccupied(List<Character> characters, Character src, Character dest, Vector2i succesor)
    {
        bool flag = false;
        foreach (Character character in characters)
        {
            if (character == src || character == dest)
                continue;
            if (character.cellPosition.Equals(succesor))
            {
                flag = true;
                break;
            }
                
        }
        return flag;
    }

    static Stack<Vector2i> TracePath(Node[,] cellDetails, Vector2i dest)
    {
        Stack<Vector2i> path = new Stack<Vector2i>();
        Vector2i current = dest;
        //Console.WriteLine("Tracing path:");
        while (!cellDetails[current.Y,current.X].parent.Equals(current))
        {
            path.Push(current);
            Vector2i temp = cellDetails[current.Y, current.X].parent;
            current = temp;
            //Console.WriteLine("Current cell: " + current.ToString());
        }
        path.Push(current);
        return path;
    }
    
}