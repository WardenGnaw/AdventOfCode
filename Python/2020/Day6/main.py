import sys
import os


def Parse_Part1(filePath):
    data = []
    answers = set()
    with open(filePath, 'r') as f:
        for line in f:
            strippedLine = line.strip()
            # New group, create a new set of answers
            if not strippedLine:
                data.append(answers)
                answers = set()
            else:
                # Save answers of group in a set
                ans = [char for char in strippedLine]
                for a in ans:
                    answers.add(a)

    if answers:
        data.append(answers)

    return data


def Parse_Part2(filePath):
    # Create a list of list of sets
    data = []
    people = []
    with open(filePath, 'r') as f:
        for line in f:
            strippedLine = line.strip()
            if not strippedLine:
                data.append(people)
                people = []
            else:
                answers = set()
                ans = [char for char in strippedLine]
                for a in ans:
                    answers.add(a)
                people.append(answers)

    if people:
        data.append(people)

    return data


def Part1(filePath):
    data = Parse_Part1(filePath)
    nums = 0
    for d in data:
        nums = len(d) + nums
    return nums


def Part2(filePath):
    data = Parse_Part2(filePath)
    count = 0
    for people in data:
        # Use person 0 as the baseline
        answers = set(people[0])

        # Look at other folks
        for person in people[1:]:
            # Cache a copy
            oldAnswers = set(answers)
            # See of this person has the same answers as person 0.
            for ans in oldAnswers:
                # Remove if they do not have the same answer
                if ans not in person:
                    answers.remove(ans)

        count = count + len(answers)
    return count


def main():
    dir_path = os.path.dirname(os.path.realpath(__file__))
    filePath = os.path.join(dir_path, 'inputs', 'input')
    print(Part1(filePath))
    print(Part2(filePath))

    return 0


if __name__ == "__main__":
    sys.exit(main())
