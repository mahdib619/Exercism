(ns bird-watcher)

(def last-week [0 2 5 3 7 8 4])
(def min-busy-day-count 5)

(defn today [birds]
  (last birds))

(defn inc-bird [birds]
  (update birds (dec (count birds)) inc))

(defn day-without-birds? [birds]
  (not (every? pos? birds)))

(defn n-days-count [birds n]
  (reduce + (take n birds)))

(defn busy-days [birds]
  (count (filter #(>= % min-busy-day-count) birds)))

(defn odd-week? [birds]
  (or (= birds [1 0 1 0 1 0 1]) (= birds [0 1 0 1 0 1 0])))
