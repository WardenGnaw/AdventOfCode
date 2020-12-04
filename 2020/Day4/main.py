import sys
import os
import re


def Parse(filePath):
    data = []
    with open(filePath, 'r') as f:
        d = {}
        for line in f:
            line = line.strip()
            if not line:
                if d:
                    data.append(d)
                d = {}
            else:
                fields = line.split(' ')
                for f in fields:
                    key, value = f.split(':')
                    key = key.strip()
                    value = value.strip()
                    if (key == "byr" or key == "iyr" or key == "eyr" or
                            key == "hgt" or key == "hcl" or key == "ecl" or
                            key == "pid" or key == "cid"):
                        d[key] = value

    return data


def Part1(filePath):
    data = Parse(filePath)

    count = 0
    for d in data:
        if len(d) == 8:
            count = count + 1
        elif len(d) == 7 and "cid" not in d:
            count = count + 1
        else:
            pass

    return count


def Part2(filePath):
    data = Parse(filePath)
    count = 0
    for d in data:
        if len(d) == 8 or (len(d) == 7 and "cid" not in d):
            birth = int(d["byr"])
            if birth < 1920 or birth > 2002:
                continue
            issue = int(d["iyr"])
            if issue < 2010 or issue > 2020:
                continue
            exp = int(d["eyr"])
            if exp < 2020 or exp > 2030:
                continue
            height = d["hgt"]
            if height.endswith("cm"):
                height = int(height[:-2])
                if height < 150 or height > 193:
                    continue
            elif height.endswith("in"):
                height = int(height[:-2])
                if height < 59 or height > 76:
                    continue
            else:
                continue

            hair = d["hcl"]
            if not re.search("^#[0-9|a-f]{6}$", hair):
                continue

            eye = d["ecl"]
            if eye not in {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"}:
                continue

            passportId = d["pid"]
            if not re.search("^[0-9]{9}$", passportId):
                continue

            count = count + 1

    return count


def main():
    dir_path = os.path.dirname(os.path.realpath(__file__))
    filePath = os.path.join(dir_path, 'inputs', 'input')
    print(Part1(filePath))
    print(Part2(filePath))

    return 0


if __name__ == "__main__":
    sys.exit(main())
