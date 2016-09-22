select a.Reference                               
,      a.Amount                                  
,      b.Reference                               
,      b.Amount                                  
,      CASE WHEN a.Amount = b.Amount THEN 'Y'
                                     ELSE 'N' END [Match]
,      CASE WHEN a.Amount <> b.Amount THEN (a.Amount - b.Amount)
                                      ELSE 0 END  [Difference]
from            ReconFrom a
left outer join ReconTo   b on a.Reference = b.Reference
