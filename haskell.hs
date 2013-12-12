-- Solutions to http://defshef.github.io/meeting-2.html

reversed :: [Int] -> [Int]
reversed []     = []
reversed (x:xs) = (reversed xs) ++ [x]

