# GraphMaker
 Graph Theory Lab MOVS

# Node
Класс ноды
1. int Number { get; }
2. List<IEdge> IncidentEdges { get; }
3. List<INode> IncidentNodes { get; }

Сами ноды создавать ручками не нужно -- это обязанности класса Graph (см. ниже).

# Edge
Класс ребра
1. INode First { get; }
2. INode Second { get; }
3. int Length { get; set; }
4. INode OtherNode(INode node);
5. bool IsIncident(INode node);

Рёбра, как и ноды, создаются методами класса Graph (см. ниже).

```C#
using GraphMaker.Model;

// Про класс Graph смотреть ниже
graph = new Graph();

var node1 = graph.AddNode();
var node2 = graph.AddNode();
var node3 = graph.AddNode();

var edge12 = graph.AddEdge(node1, node2);

// Проверка на то, инцидентная ли заданная нода ребру
edge12.IsIncident(node1); // вернёт true
edge12.IsIncident(node3); // вернёт false

// Возвращает другой конец ребра
var n1 = edge12.OtherNode(node1); // вернёт node2
var n2 = edge12.OtherNode(node2); // вернёт node1
var n3 = edge12.OtherNode(node3); // выкинет ArgumentException 
```

# Graph
Собственно сам класс графа.
Что умеет и имеет:
1. INode AddNode();
2. void DeleteNode(INode v);
3. IEdge AddEdge(INode v1, INode v2, int length);
4. void DeleteEdge(IEdge edge);
5. IReadOnlyList<INode> Nodes { get; }
6. IReadOnlyList<IEdge> Edges { get; }
7. event GraphChangeEvent Changed;

```C#
using GraphMaker.Model;

// Создаём сам граф
graph = new Graph();

// Добавление нод
var node1 = graph.AddNode();
var node2 = graph.AddNode();

// Добавление рёбер между node1 и node2 с длиной 10 (по умолчанию длина 1)
var edge12 = graph.AddEdge(node1, node2, 10);

// Повторное добавление того же ребра просто вернёт edge12 (при этом в списки ничего не добавится)
var edge12_1 = graph.AddEdge(node1, node2);

// Создание петлей запрещено -- выкинет ArgumentException
var edge11 = graph.AddEdge(node1, node1);

// Получение списка всех нод
var nodes = graph.Nodes;

// Если охота уметь изменять, то можно вот так:
var nodesList = graph.Nodes.ToList();

// Список всех рёбер
var edges = graph.Edges;

// Удаление ребра
graph.DeleteEdge(edge12);

// Удаление ноды (естественно удаляет также все инцидентные рёбра)
graph.DeleteNode(node1);
```

# Обходы графа
Обходы реализованы в виде методов-расширений для нод.

```C#
// Получаем все ноды графа
var nodes = graph.Nodes;

// Берём первую
var first = nodes.First();

// Лист в котором лежат все ноды в порядке их обхода в глубину начиная с first
var depthOrder = first.DepthSearch();

// Лист в котором лежат все ноды в порядке их обхода в ширину начиная с node[1]
var breadthOrder = nodes[1].BreadthSearch();
```

# Другие алгоритмы
Также должны быть реализованы в виде методов-расширений. Смотреть файл GraphExtensions.cs.

```C#
// Минимальное остовное дерево
var mst = graph.MST();

// Минимальное расстояние между вершинами
var minPath = graph.MinPath(v1, v2);

// Число компонент связности
var cccount = graph.CCcount();
```