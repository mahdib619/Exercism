"""Determine if a word or phrase is an isogram"""


def is_isogram(string):
    visited = set()

    return all(map(lambda ch: False if ch in visited else not ch.isalpha() or visited.add(ch) == None,
               string.lower()))
