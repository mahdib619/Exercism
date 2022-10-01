class HighScores:
    def __init__(self, scores):
        self.scores = scores
        self.sorted_scores = sorted(scores, reverse=True)

    def latest(self):
        return self.scores[-1]

    def personal_best(self):
        return self.sorted_scores[0]

    def personal_top_three(self):
        return self.sorted_scores[:3]