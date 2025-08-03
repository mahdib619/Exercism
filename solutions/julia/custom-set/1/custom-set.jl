CustomSet, complement!, complement = Set, setdiff!, setdiff
disjoint(set1::Set, set2::Set) = isempty(intersect(set1, set2))