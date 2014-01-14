; Solutions to http://defshef.github.io/meeting-2.html

; ****************
; **** Warmup ****
; ****************

; Normally
(defn doubled
  "double all items in a list"
  [l]
  (map #(* % 2) l))

(defn evens [l]
  "Filter out even numbers from a list"
  (filter even? l))

(defn multiplied [l]
  "multiply together all numbers in a list"
  (reduce * l))

; Using partial
(def doubled (partial map (partial * 2)))
(def evens (partial filter even?))
(def multiplied (partial reduce *))

(defn reverse-list [[h & tail]]
  "reverse a list"
  (if-not h
    []
    (conj (reverse-list tail) h)))

; Recursion in clojure can stackoverflow unless we use loop..recur
(defn reverse-list' [l]
  "reverse a list"
  (loop [[h & tail] l
         acc        nil]
    (if-not h
      acc
      (recur tail (cons h acc)))))

(defn get-element [[h & tail] n]
  "get the nth element from a list"
  (if (= 1 n)
    h
    (get-element tail (dec n))))

(defn get-element' [l n]
  "get the nth element from a list"
  (loop [[h & tail] l
         n          n]
    (if (= 1 n)
      h
      (recur tail (dec n)))))

; ******************
; **** Beginner ****
; ******************

(->> init
    (f1 b c d %)
    (f2 b c d %))

(defn odd-square-sum [l]
  "sum together the squres of all odd numbers in a list"
  (->> l
       (filter odd?)
       (map #(* % %))
       (reduce +)))

(defn slice [[h & tail] s n]
  "Return n items of a list starting from an offset s"
  (cond
    (zero? n) nil
    (zero? s) (cons h (slice tail s (dec n)))
    :else     (slice tail (dec s) n)))

(defn slice' [l s n]
  "Return n items of a list starting from an offset s"
  (loop [[h & tail] l
         s          s
         n          n
         acc        []]
    (cond
      (zero? n) acc
      (zero? s) (recur tail s       (dec n) (conj acc h))
      :else     (recur tail (dec s) n       acc))))

(defn fib [n]
  "Return nth fibonacci number"
  (if (<= n 2)
    1
    (+ (fib (- n 1)) (fib (- n 2)))))

; Slooow above n = 35
(defn fibn [n]
  "Return the first n fibonacci numbers (slow when n >= 35)"
  (if (zero? n)
    []
    (conj (fibn (dec n)) (fib n))))

(def fib'
  "Return nth fibonacci number (faster)"
  (memoize
    (fn [n]
      (if (<= n 2)
        1
        (+ (fib' (- n 1)) (fib' (- n 2)))))))

; Still fast up to n = 92, then numbers get too big for a long integer
(defn fibn' [n]
  "Return the first n fibonacci numbers (faster)"
  (if (zero? n)
    []
    (conj (fibn' (dec n)) (fib' n))))

; *********************
; **** Experienced ****
; *********************

(defn flat [[h & tail]]
  "flatten a list"
  (cond
    (nil? h)  nil
    (coll? h) (concat (flat h) (flat tail))
    :else     (cons h (flat tail))))

(defn flat' [l]
  "flatten a list"
  (loop [[h & tail] l
         acc        []]
    (cond
      (nil? h)  acc
      (coll? h) (recur tail (concat acc (flat h)))
      :else     (recur tail (conj acc h)))))

(defn substitute [a b [h & tail]]
  "replace a with b in a list"
  (cond
    (nil? h)  nil
    (coll? h) (cons (substitute a b h) (substitute a b tail))
    (= a h)   (cons b (substitute a b tail))
    :else     (cons h (substitute a b tail))))

(defn substitute' [a b l]
  "replace a with b in a list"
  (loop [[h & tail] l
         acc        []]
    (cond
      (nil? h)  acc
      (coll? h) (recur tail (conj acc (substitute a b h)))
      (= a h)   (recur tail (conj acc b))
      :else     (recur tail (conj acc a)))))

(defn deep-map [f [h & tail]]
  "map over a nested list"
  (cond
    (nil? h)  nil
    (coll? h) (cons (deep-map f h) (deep-map f tail))
    :else     (cons (f h) (deep-map f tail))))

(defn deep-map' [f l]
  "map over a nested list"
  (loop [[h & tail] l
         acc        []]
    (cond
      (nil? h)  acc
      (coll? h) (recur tail (conj acc (deep-map' f h)) )
      :else     (recur tail (conj acc (f h))))))

(defn deep-filter [f [h & tail]]
  "filter a nested list"
  (cond
    (nil? h)  nil
    (coll? h) (if-let [inner (deep-filter f h)]
                (cons inner (deep-filter f tail))
                (deep-filter f tail))
    (f h)     (cons h (deep-filter f tail))
    :else     (deep-filter f tail)))

(defn deep-filter' [f l]
  "filter a nested list"
  (loop [[h & tail] l
         acc        []]
    (cond
      (nil? h)  acc
      (coll? h) (recur tail (if-let [inner (seq (deep-filter' f h))]
                              (conj acc (vec inner))
                              acc))
      (f h)     (recur tail (conj acc h))
      :else     (recur tail acc))))

(defn deep-reduce
  "reduce a nested list"
  ([f [h & l]]
   (deep-reduce f h l))
  ([f m [h & tail]]
   (cond
     (nil? h)  m
     (coll? h) (deep-reduce f (deep-reduce f m h) tail)
     :else     (deep-reduce f (f m h) tail))))

(defn deep-reduce'
  "reduce a nested list"
  ([f [h & l]]
   (deep-reduce' f h l))
  ([f m l]
   (loop [m          m
          [h & tail] l]
     (cond
       (nil? h)  m
       (coll? h) (recur (deep-reduce' f m h) tail)
       :else     (recur (f m h) tail)))))

(defn odd-square-sum-deep
  "sum together the squres of all odd numbers in a nested list"
  [l]
  (->> (deep-filter odd? l)
       (deep-map #(* % %))
       (deep-reduce +)))

(defn odd-square-sum-deep'
  "sum together the squres of all odd numbers in a nested list"
  [l]
  (->> (deep-filter' odd? l)
       (deep-map' #(* % %))
       (deep-reduce' +)))

; *********************
; ****** Expert *******
; *********************

(defn- extract-lines
  "all possible lines on a tic-tac-toe board"
  [board]
  [;rows
   (nth board 0) (nth board 1) (nth board 2)
   ;cols
   (map #(nth % 0) board) (map #(nth % 1) board) (map #(nth % 2) board)
   ; diagonals
   [(get-in board [0 0]) (get-in board [1 1]) (get-in board [2 2])]
   [(get-in board [0 2]) (get-in board [1 1]) (get-in board [2 0])]])

(defn- three?
  "Is this three :x or three :o?"
  [l]
  (cond
    (= [:x :x :x] l) :x
    (= [:o :o :o] l) :o
    :else nil))

(defn winner [board]
  "Who won tic-tac-toe?"
  (let [lines (extract-lines board)]
    (some three? lines)))

(defn defshef [expr]
  "DSL evaluator for defshef language"
  (cond
    (nil? expr) expr
    (number? expr) expr
    (number? (first expr)) expr
    :else (let [[action expr1 expr2] expr
                arg1 (defshef expr1)
                arg2 (defshef expr2)]
            (case action
              cowering (* 2 arg1)
              burrito (repeat arg2 arg1)
              tap (inc arg1)
              steel (dec arg1)
              sheffield (concat arg1 [arg2])
              meatspace (first arg1)
              geek (concat arg1 arg2)))))
