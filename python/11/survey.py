class AnonymousSurvery():
    """收集匿名调查问卷的答案"""

    def __init__(self, questin):
        """存储一个问题，并未存储答案做准备"""
        self.question = questin
        self.responses = []

    def show_question(self):
        """显示要调查的问题"""
        print(self.question)

    def store_response(self, response):
        """存储答案"""
        self.responses.append(response)

    def show_results(self):
        """显示答案"""
        print("Survery results:")
        for response in self.responses:
            print("- " + response)
