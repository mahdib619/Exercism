"""Pangram checker"""

def is_pangram(sentence):
    unique_letters = set([ch.lower() for ch in sentence if ch.isalpha()])
    return len(unique_letters) == 26
