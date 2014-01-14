-- Solutions to http://defshef.github.io/meeting-2.html

-- Warmup

doubled :: Real a => [a] -> [a]
doubled = map (2 *)

evens = filter even

multiplied = foldl1 (*)

reverseList []     = []
reverseList (x:xs) = (reverseList xs) ++ [x]

getElement []     _ = error "Not found"
getElement (x:xs) 1 = x
getElement (x:xs) n = getElement xs (n - 1)

-- Beginner

oddSquareSum = (foldl1 (+)) . (map (\x -> x * x)) . (filter odd)

slice     l  _ 0 = []
slice (x:xs) 0 j = x : (slice xs 0 (j - 1))
slice (x:xs) i j = slice xs (i - 1) j

fibn 1 = [1]
fibn 2 = [1, 1]
fibn n = sofar ++ [me]
    where
        sofar = fibn (n - 1)
        me = (last sofar) + (last $ init sofar)

-- Experienced

data Nested a = Item a | List [Nested a] deriving (Show)

flatten :: Nested a -> [a]
flatten (List []) = []
flatten (List ((Item a):xs)) = a : (flatten (List xs))
flatten (List ((List a):xs)) = (flatten (List a)) ++ (flatten (List xs))

substitute :: Eq a => a -> a -> Nested a -> Nested a
substitute _ _ (List []) = List []
substitute a b (List ((Item x):xs))
    | x == a    = List ((Item b) : recursed)
    | otherwise = List ((Item x) : recursed)
    where
        (List recursed) = substitute a b (List xs)
substitute a b (List ((List l):xs)) = List (headRecurse : tailRecurse)
    where
        headRecurse = substitute a b (List l)
        (List tailRecurse) = substitute a b (List xs)

-- Expert

data DefShef =
    N Int
    | L [Int]
    | Cowering DefShef
    | Burrito DefShef DefShef
    | Tap DefShef
    | Steel DefShef
    | Sheffield DefShef DefShef
    | Meatspace DefShef
    | Geek DefShef DefShef
    deriving (Show)

defshef :: DefShef -> DefShef

defshef (N n) = N n
defshef (L l) = L l
defshef (Cowering expr) = N (n * 2)
    where
        (N n) = defshef expr
defshef (Burrito expr1 expr2) = L (take m (repeat n))
    where
        (N n) = defshef expr1
        (N m) = defshef expr2
defshef (Tap expr) = N (n + 1)
    where
        (N n) = defshef expr
defshef (Steel expr) = N (n - 1)
    where
        (N n) = defshef expr
defshef (Sheffield expr1 expr2) = L (l ++ [n])
    where
        (L l) = defshef expr1
        (N n) = defshef expr2
defshef (Meatspace expr) = N n
    where
        (L (n:_)) = defshef expr
defshef (Geek expr1 expr2) = L (l1 ++ l2)
    where
        (L l1) = defshef expr1
        (L l2) = defshef expr2
