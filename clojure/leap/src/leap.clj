(ns leap)

(defn divisible?
  [x y] (= (mod x y) 0))

(defn leap-year?
  "Returns true if `year` is a leap year."
  [year] (if (divisible? year 100) (divisible? year 400) (divisible? year 4)))