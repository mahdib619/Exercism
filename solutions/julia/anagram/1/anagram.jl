detect_anagrams(subject, candidates) = (slst = getlst(subject); candidates[candidates .|> c -> is_anagram(subject, slst, c)])
is_anagram(sub, sublst, cand) = lowercase(sub) != lowercase(cand) && sublst == getlst(cand)
getlst(str) = str |> lowercase |> collect |> sort