;1. Double all the numbers in a list

( defn doubled [xs] ( map #(* % 2) xs ) )
;=> ( doubled [ 1 2 3 4 ] )
;(2 4 6 8)


;2. Extract all the even numbers from a list

( defn evens [xs] ( filter #(== 0 (mod % 2)) xs) )
;=> (evens [ 1 2 3 4 5 6 7 ])
;(2 4 6)

;3. Multiply all the numbers in a list together

(defn multiplied [xs] (reduce * xs) )
;=> (multiplied [ 1 2 3 4 5 ])
;120

;4. Reverse a list

( defn reverse-list [xs]
    (
        if (empty? xs) xs
        ( cons (last xs) (reverse-list (butlast xs)) )
    )
)
;=> ( reverse-list [ 1 2 3 4 5 ])
;(5 4 3 2 1)

;5. Get the nth element in a list where the first item is '1'

( defn get-element [xs i] ( last ( take i xs ) ) )
;=> ( get-element [ 5 6 7 8 ] 3 )
;7

;Beginner

;1. Calculate the sum of the squares of all odd numbers in a list

( defn odd-square-sum [xs] (
    reduce + (
        map #(* % %) (
            filter #(== 1 (mod % 2)) xs
        )
    )
))
;=> (odd-square-sum [ 4 5 6 7 8 9 ])
;155

;2. Extract a slice from a list

( defn slice [xs s e] (
    drop s (
        take (+ s e) xs
    )
))
;=>  (slice [ 3 4 5 6 7 8 9 ] 2 4)
;(5 6 7 8)

( defn slice [ s e [ x & xs ] ]
  ( cond
    ( zero? e ) nil
    ( zero? s ) ( cons x ( slice s ( dec e ) xs ) )
    :else       ( slice ( dec s ) ( dec e ) xs ) ) )
;=> ( slice 2 5 [ 1 2 3 4 5 6 7 ] )
;(3 4 5)

;3. Generate the first n items of the fibonacci series.

(defn fibn [n] ( take n (
    ( fn __fib [a b] ( lazy-seq ( cons a ( __fib b ( + a b ) ) ) ) ) 1 1
)))

;=> (fibn 9)
;(1 1 2 3 5 8 13 21 34)

;Experienced

;1. Flatten a nested list

(defn cflatten [xs] (
    if (empty? xs) xs (
        if (coll? (first xs)) (
            concat (cflatten (first xs)) (cflatten (rest xs))
        ) (
            cons (first xs) (cflatten (rest xs))
        )
    )
))
;=> (cflatten [1 2 [3 4 [ 5 6 7 [ 8 9 ] 10 ] 11 12 [ 13 14 ] 15 ] 16 17 ])
;(1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17)

;2. Replace all occurences of an item with another in a nested list

(defn substitute [o s xs]
    (map (fn [x]
        (if (coll? x)
            (substitute o s x)
            (if (= x o) s x)
        )
    ) xs))
;=> (substitute 2 5 [ 1 2 3 [ 1 2 2 3 ] [ 1 [ 2 ] 3 ] ])
;(1 5 3 (1 5 5 3) (1 (5) 3))
