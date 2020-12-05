import sys
import os


def Parse(filePath):
    data = []
    with open(filePath, 'r') as f:
        for line in f:
            strippedLine = line.strip()
            data.append(strippedLine)

    return data


def CalculateSeatId(partition):
    row = 0
    col = 0
    min_range = 0
    max_range = 127
    for rowData in partition[:-4]:
        middle = int((max_range + min_range) / 2)
        if rowData == "F":
            max_range = middle
        elif rowData == "B":
            min_range = middle + 1

    if partition[-4] == "F":
        row = min_range
    elif partition[-4] == "B":
        row = max_range

    min_range = 0
    max_range = 7

    for colData in partition[-3:-1]:
        middle = int((max_range + min_range) / 2)
        if colData == "L":
            max_range = middle
        elif colData == "R":
            min_range = middle + 1

    if partition[-1] == "L":
        col = min_range
    elif partition[-1] == "R":
        col = max_range

    return row * 8 + col


def GetSeatIds(data):
    results = []

    for d in data:
        results.append(CalculateSeatId(d))

    return results


def Part1(filePath):
    data = Parse(filePath)
    results = GetSeatIds(data)

    return max(results)


def Part2(filePath):
    data = Parse(filePath)
    results = GetSeatIds(data)

    for i in range(min(results), max(results)):
        if i not in results:
            return i


def main():
    dir_path = os.path.dirname(os.path.realpath(__file__))
    filePath = os.path.join(dir_path, 'inputs', 'input')
    print(Part1(filePath))
    print(Part2(filePath))

    return 0


if __name__ == "__main__":
    sys.exit(main())
