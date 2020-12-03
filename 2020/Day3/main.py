import sys
import os
from functools import reduce


def Parse(filename):
    map_data = []
    with open(filename, 'r') as f:
        for line in f:
            row = list(line.strip())
            map_data.append(row)

    return map_data


def TreesHit(map_data, downRate, rightRate):
    index = {
        "row": 0,
        "col": 0
    }
    tree = 0
    length = len(map_data[0])
    while True:
        index["row"] += downRate
        index["col"] += rightRate
        if (index["col"] >= length):
            index["col"] = index["col"] % length

        if index["row"] >= len(map_data):
            break

        if (map_data[index["row"]][index["col"]] == '#'):
            tree += 1

    return tree


def Part1(filename):
    map_data = Parse(filename)
    return TreesHit(map_data, 1, 3)


def Part2(filename):
    indexes = [
        {
            "row": 1,
            "col": 1
        },
        {
            "row": 1,
            "col": 3
        },
        {
            "row": 1,
            "col": 5
        },
        {
            "row": 1,
            "col": 7
        },
        {
            "row": 2,
            "col": 1
        },
    ]
    trees = []
    map_data = Parse(filename)
    for incIndex in indexes:
        trees.append(TreesHit(map_data, incIndex["row"], incIndex["col"]))

    result = reduce((lambda x, y: x * y), trees)

    return result


def main():
    dir_path = os.path.dirname(os.path.realpath(__file__))
    filename = os.path.join(dir_path, 'inputs', 'input')

    print("Part 1: ", Part1(filename))
    print("Part 2: ", Part2(filename))

    return 0


if __name__ == "__main__":
    sys.exit(main())
