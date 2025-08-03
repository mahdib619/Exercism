(ns log-levels
  (:require [clojure.string :as str]))

(def split-regex #"(?=\[.*\])*: ")

(defn split-log
  [log-line]
  (str/split log-line split-regex))

(defn message
  "Takes a string representing a log line
   and returns its message with whitespace trimmed."
  [log-line]
  (str/trim (get (split-log log-line) 1))
  )

(defn log-level
  "Takes a string representing a log line
   and returns its level in lower-case."
  [log-line]
  (str/lower-case (str/replace (get (split-log log-line) 0) #"[\[\]]" ""))
  )

(defn reformat
  "Takes a string representing a log line and formats it
   with the message first and the log level in parentheses."
  [log-line]
  (str (message log-line) " (" (log-level log-line) ")")
  )
