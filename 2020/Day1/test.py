import unittest
import os

import main


class Day1(unittest.TestCase):
    def test_part1(self):
        dir_path = os.path.dirname(os.path.realpath(__file__))
        data = []
        with open(os.path.join(dir_path, 'inputs', 'test_input'), 'r') as f:
            for line in f:
                data.append(int(line.strip()))

        self.assertEqual(main.Part1(data), 514579)

    def test_part2(self):
        dir_path = os.path.dirname(os.path.realpath(__file__))
        data = []
        with open(os.path.join(dir_path, 'inputs', 'test_input'), 'r') as f:
            for line in f:
                data.append(int(line.strip()))

        self.assertEqual(main.Part2(data), 241861950)


if __name__ == '__main__':
    unittest.main()
