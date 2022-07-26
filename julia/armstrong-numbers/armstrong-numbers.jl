isarmstrong(num) = (ns = string(num); sum(map(a -> parse(Int, a)^length(ns), split(ns, ""))) == num)
