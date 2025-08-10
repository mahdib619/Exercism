(ns squeaky-clean
  (:require [clojure.string :as str]))

(defn fix-spaces
  [s]
  (str/replace s #" " "_"))

(defn fix-controlchars
  [s]
  (str/replace s Character/is "CTRL"))

(defn clean
  "fixes invalid varaiable names."
  [s]
  (fix-controlchars (fix-spaces s)))