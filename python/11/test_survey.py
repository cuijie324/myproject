import unittest
from survey import AnonymousSurvery


class TestAnonymousSurvery(unittest.TestCase):

    def setUp(self):
        question = "Which language did you first learn to speak?"
        self.my_survey = AnonymousSurvery(question)
        self.responses = ['English', 'Chinese', 'French']

    def test_store_single_response(self):
        self.my_survey.store_response(self.responses[0])
        self.assertIn(self.responses[0], self.my_survey.responses)

    def test_store_three_response(self):
        for response in self.responses:
            self.my_survey.store_response(response)

        for response in self.responses:
            self.assertIn(response, self.my_survey.responses)


unittest.main()
