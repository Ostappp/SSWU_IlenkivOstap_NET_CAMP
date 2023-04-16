using System.Data;
using System.Linq;
using System.Reflection;

namespace Homework5
{
    internal class Task1
    {
        List<(float, float)> _treeCoords;
        List<(float, float)> _fencePoints;
        public double FenceLength { get => FenceLenght(); }
        public Task1(IEnumerable<(float, float)> treeCoords)
        {
            if (treeCoords.Count() < 1)
            {
                throw new ArgumentException("Tree count must be grather or equal to 1");
            }
            _treeCoords = treeCoords.ToList();
            _fencePoints = GenerateFence();
        }
        public Task1(int treeCount, int minMaxRange = 50)
        {
            if (treeCount < 1)
            {
                throw new ArgumentException("Tree count must be grather or equal to 1");
            }

            _treeCoords = GenerateGarden(treeCount, minMaxRange);
            _fencePoints = GenerateFence();
        }
        List<(float, float)> GenerateGarden(int treeCount, int minMaxRange)
        {
            Random rnd = new Random();
            List<(float, float)> trees = new List<(float, float)>(treeCount);
            for (int i = 0; i < treeCount; i++)
            {
                trees.Add(new((float)rnd.NextDouble() * minMaxRange * 2 - minMaxRange, (float)rnd.NextDouble() * minMaxRange * 2 - minMaxRange));
            }
            trees = trees.DistinctBy(t => t).ToList();
            while (trees.Count < treeCount)
            {
                trees.Add(new((float)rnd.NextDouble() * minMaxRange * 2 - minMaxRange, (float)rnd.NextDouble() * minMaxRange * 2 - minMaxRange));
                trees = trees.DistinctBy(t => t).ToList();
            }

            return trees;
        }
        List<(float, float)> GenerateFence()
        {
            List<(float, float)> fence_coord = new List<(float, float)>();
            //знаходимо крайні дерева
            List<List<(float, float)>> cornersBorder = new List<List<(float, float)>>
            {   new List<(float,float)>(), new List<(float,float)>(), new List<(float,float)>(), new List<(float,float)>(),
                new List<(float,float)>(), new List<(float,float)>(), new List<(float,float)>(), new List<(float,float)>()
            };
            List<(float, float)> localEdgeTrees = GetEdgeTrees(_treeCoords);
            List<(float, float)> treeInSector = _treeCoords.Where(t => IsTreeInsideSector(localEdgeTrees[0], localEdgeTrees[1], t)).Select(t => t).ToList();
            List<(float, float)> temp;
            int treesOnEdge = 8;//8-стала. Саме стільки потрібно розглянути дерев щоб точно визначити межі
            for (int i = 0; i < treesOnEdge; i++)
            {
                List<(float, float)> firstToAdd = new List<(float, float)>();
                List<(float, float)> lastToAdd = new List<(float, float)>();

                while (treeInSector.Count > 0)
                {

                    temp = treeInSector.Where(t => IsTreeInsideSector(localEdgeTrees[i], localEdgeTrees[(i + 1) % treesOnEdge], t)).Select(t => t).ToList();
                    if (temp.Count == 0)
                        break;
                    //знаходимо 8 крайні дерева
                    localEdgeTrees = GetEdgeTrees(temp);
                    if (localEdgeTrees.Count > 0)
                    {
                        firstToAdd.Add(localEdgeTrees[i]);
                        lastToAdd.Add(localEdgeTrees[(i + 1) % treesOnEdge]);

                    }
                    //лишаємо лише ті, що лежать ззовні від діагоналі
                    treeInSector = treeInSector.Where(t => IsTreeInsideTopOrDownDiagonalArea(localEdgeTrees[i], localEdgeTrees[(i + 1) % treesOnEdge], t, i > 4)).Select(t => t).ToList();

                }
                if (firstToAdd.Count > 0)
                    cornersBorder[i].AddRange(firstToAdd);

                if (firstToAdd.Count > 0)
                {
                    lastToAdd.Reverse();
                    cornersBorder[i].AddRange(lastToAdd);
                }
            }
            localEdgeTrees = GetEdgeTrees(_treeCoords);
            for (int i = 0; i < treesOnEdge; i++)
            {
                fence_coord.Add(localEdgeTrees[i]);
                if (cornersBorder[i].Count > 0)
                    fence_coord.AddRange(cornersBorder[i]);
            }

            return fence_coord.Distinct().ToList();
        }
        /// <summary>
        /// Серед наданих дерев визначає крайні
        /// </summary>
        /// <returns>повертає список крайніх дерев за годинниковою стрічкою, крайні лівий(верхній), верхній(лівий), верхній(правий), правий(верхній), правий(нижній), нижній(правий), нижній(лівий), лівий(нижній).</returns>
        List<(float, float)> GetEdgeTrees(List<(float, float)> treeInArea)
        {
            List<(float, float)> edgeTrees = new List<(float, float)>(8);

            treeInArea = treeInArea.OrderBy(t => t.Item1).ThenBy(t => t.Item2).ToList();
            var leftDown = treeInArea.First();
            var righUp = treeInArea.Last();

            treeInArea = treeInArea.OrderBy(t => t.Item1).ThenByDescending(t => t.Item2).ToList();
            var leftUp = treeInArea.First();
            var righDown = treeInArea.Last();

            treeInArea = treeInArea.OrderBy(t => t.Item2).ThenBy(t => t.Item1).ToList();
            var bottomLeft = treeInArea.First();
            var topRight = treeInArea.Last();

            treeInArea = treeInArea.OrderBy(t => t.Item2).ThenByDescending(t => t.Item1).ToList();
            var bottomRight = treeInArea.First();
            var topLeft = treeInArea.Last();



            edgeTrees.Add(leftUp);
            edgeTrees.Add(topLeft);
            edgeTrees.Add(topRight);
            edgeTrees.Add(righUp);
            edgeTrees.Add(righDown);
            edgeTrees.Add(bottomRight);
            edgeTrees.Add(bottomLeft);
            edgeTrees.Add(leftDown);

            return edgeTrees;
        }
        bool IsTreeInsideSector((float, float) cornerA, (float, float) cornerB, (float, float) tree)
        {
            (float minX, float maxX) = (Math.Min(cornerA.Item1, cornerB.Item1),
                                        Math.Max(cornerA.Item1, cornerB.Item1));

            (float minY, float maxY) = (Math.Min(cornerA.Item2, cornerB.Item2),
                                        Math.Max(cornerA.Item2, cornerB.Item2));

            float x = tree.Item1;
            float y = tree.Item2;

            return !tree.Equals(cornerA) && !tree.Equals(cornerB) &&
                x >= minX && x <= maxX && y >= minY && y <= maxY;
        }
        /// <summary>
        /// проводимо діагональ через прямокутник. Яка саме діагональ утвориться залежить від вхідних координат/
        /// 
        /// </summary>
        /// <param name="cornerA"></param>
        /// <param name="cornerB"></param>
        /// <param name="tree"></param>
        /// <param name="isUnderLine">В залежності від булевого значення перевірятимемо чи дерево є в верхьому/нижньому трикутнику чи ні</param>
        /// <returns></returns>
        bool IsTreeInsideTopOrDownDiagonalArea((float, float) cornerA, (float, float) cornerB, (float, float) tree, bool isUnderLine)
        {
            (float minX, float maxX) = (Math.Min(cornerA.Item1, cornerB.Item1),
                                        Math.Max(cornerA.Item1, cornerB.Item1));

            (float minY, float maxY) = (Math.Min(cornerA.Item2, cornerB.Item2),
                                        Math.Max(cornerA.Item2, cornerB.Item2));

            float x = tree.Item1;
            float y = tree.Item2;

            //кутовий коефіцієнт діагонеалі
            float k = (maxY - minY) / (maxX - minX);

            //маємо пряму, паралельну до діагоналі, що проходить через точку, яку нам потрібно проаналізувати
            //знаходимо координату перетину нової прямої з віссю Y
            float b = maxY - k * maxX;

            //знаходимо значення координати y точки, яка належить новій прямій та з координатою x,
            //яка співпадає з аналогічною координатою точки, що аналізуємо.
            float yCoordWithSameX = k * x + b;

            //викидаємо дерева що стоять на кінцях діагоналі, ті що лежать на діагоналі та ті що не над/під.
            return !tree.Equals(cornerA) && !tree.Equals(cornerB) &&
                isUnderLine ? y < yCoordWithSameX : y > yCoordWithSameX;

        }
        double FenceLenght()
        {
            int fenceCount = _fencePoints.Count;
            if (fenceCount == 1)
                return 0.1;

            double length = 0;
            for (int i = 0; i < fenceCount; i++)
            {
                length += Math.Sqrt
                    (
                        (_fencePoints[i].Item1 - _fencePoints[(i + 1) % fenceCount].Item1) *
                        (_fencePoints[i].Item1 - _fencePoints[(i + 1) % fenceCount].Item1)
                        +
                        (_fencePoints[i].Item2 - _fencePoints[(i + 1) % fenceCount].Item2) *
                        (_fencePoints[i].Item2 - _fencePoints[(i + 1) % fenceCount].Item2)
                    );
                
            }
            return length;
        }
        public static bool operator ==(Task1 garden1, Task1 garden2) => garden1.FenceLength == garden2.FenceLength;
        public static bool operator !=(Task1 garden1, Task1 garden2) => !(garden1 == garden2);
        public static bool operator >=(Task1 garden1, Task1 garden2) => garden1.FenceLength >= garden2.FenceLength;
        public static bool operator <=(Task1 garden1, Task1 garden2) => garden1.FenceLength <= garden2.FenceLength;
        public static bool operator >(Task1 garden1, Task1 garden2) => !(garden1 <= garden2);
        public static bool operator <(Task1 garden1, Task1 garden2) => !(garden1 >= garden2);
    }
}
