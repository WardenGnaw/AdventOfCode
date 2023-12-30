import unittest
import os

import main


class Day5(unittest.TestCase):
    def test_part1(self):
        dir_path = os.path.dirname(os.path.realpath(__file__))
        file_path = os.path.join(dir_path, 'inputs', 'test_input')

        self.assertEqual(main.Part1(file_path), 357)


if __name__ == '__main__':
    unittest.main()
