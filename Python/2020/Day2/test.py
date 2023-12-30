import unittest
import os

import main


class Day2(unittest.TestCase):
    def test_part1(self):
        dir_path = os.path.dirname(os.path.realpath(__file__))
        filename = os.path.join(dir_path, 'inputs', 'test_input')

        self.assertEqual(main.Part1(filename), 2)

    def test_part2(self):
        dir_path = os.path.dirname(os.path.realpath(__file__))
        filename = os.path.join(dir_path, 'inputs', 'test_input')

        self.assertEqual(main.Part2(filename), 1)


if __name__ == '__main__':
    unittest.main()
