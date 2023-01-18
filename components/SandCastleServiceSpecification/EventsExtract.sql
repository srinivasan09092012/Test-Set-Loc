select e.*,m.module_name 
from ua3_integration.module_events e join ua3_integration.module m on e.module_id = m.module_id
where e.is_active = 1 and m.is_Active = 1