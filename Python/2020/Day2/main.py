import sys
import os


def Parse(filename):
    data = []
    with open(filename, 'r') as f:
        for line in f:
            policy, password = line.strip().split(":")
            _range, char = policy.split(" ")
            lower, higher = _range.split('-')
            data.append({
                "password": password.strip(),
                "lower": int(lower),
                "higher": int(higher),
                "char": char
            })

    return data


def Part1(filename):
    data = Parse(filename)
    count = 0
    for d in data:
        if (d['lower'] <= d['password'].count(d['char']) <= d['higher']):
            count += 1

    return count


def Part2(filename):
    data = Parse(filename)
    count = 0
    for d in data:
        first = d['password'][d['lower'] - 1] == d['char']
        second = d['password'][d['higher'] - 1] == d['char']
        if (first ^ second):
            count += 1

    return count


def main():
    dir_path = os.path.dirname(os.path.realpath(__file__))
    filename = os.path.join(dir_path, 'inputs', 'input')

    print("Part 1: ", Part1(filename))
    print("Part 2: ", Part2(filename))

    return 0


if __name__ == "__main__":
    sys.exit(main())
