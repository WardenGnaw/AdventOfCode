import sys
import os


def Part1(data):
    for i in range(0, len(data)):
        for j in range(i + 1, len(data)):
            if (data[i] + data[j] == 2020):
                return data[i] * data[j]


def Part2(data):
    for i in range(0, len(data)):
        for j in range(i + 1, len(data)):
            for k in range(j + 1, len(data)):
                if (data[i] + data[j] + data[k] == 2020):
                    return data[i] * data[j] * data[k]


def main():
    dir_path = os.path.dirname(os.path.realpath(__file__))

    data = []
    with open(os.path.join(dir_path, 'inputs', 'input'), 'r') as f:
        for line in f:
            data.append(int(line.strip()))

    print("Part 1: ", Part1(data))
    print("Part 2: ", Part2(data))

    return 0


if __name__ == "__main__":
    sys.exit(main())
