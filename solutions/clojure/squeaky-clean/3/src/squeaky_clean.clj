(ns squeaky-clean
  (:require [clojure.string :as str]))

(defn fix-spaces
  [s]
  (str/replace s #" " "_"))

(defn fix-controlchars
  [s]
  (str/replace s #"\p{IsControl}" "CTRL"))

(defn camel-case
  [s]
  (str/replace s #"-." #(str/upper-case (last %))))

(defn remove-non-letters
  [s]
  (str/replace s #"[^_\p{IsLetter}]" ""))

(defn remove-lower-case-greek
  [s]
  (str/replace s #"[\p{IsGreek}&&\p{IsLowercase}]" ""))

(defn clean
  "fixes invalid varaiable names."
  [s]
  (-> s
      fix-spaces
      fix-controlchars
      camel-case
      remove-non-letters
      remove-lower-case-greek))