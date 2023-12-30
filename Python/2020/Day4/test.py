import unittest
import os

import main


class Day4(unittest.TestCase):
    def test_part1(self):
        dir_path = os.path.dirname(os.path.realpath(__file__))
        file_path = os.path.join(dir_path, 'inputs', 'test_input')

        self.assertEqual(main.Part1(file_path), 2)

    def test_part2_vaid(self):
        dir_path = os.path.dirname(os.path.realpath(__file__))
        file_path = os.path.join(dir_path, 'inputs', 'valid')

        self.assertEqual(main.Part2(file_path), 4)

    def test_part2_invalid(self):
        dir_path = os.path.dirname(os.path.realpath(__file__))
        file_path = os.path.join(dir_path, 'inputs', 'invalid')

        self.assertEqual(main.Part2(file_path), 0)


if __name__ == '__main__':
    unittest.main()
