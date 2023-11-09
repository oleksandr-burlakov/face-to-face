import { Icon } from '@iconify/react';
import React, { forwardRef } from 'react';

import Box from '@mui/material/Box';

// ----------------------------------------------------------------------

const Iconify = forwardRef(({ icon, width = 20, sx, ...other } : IconifyPropTypes, ref) => (
  <Box
    ref={ref}
    component={Icon}
    className="component-iconify"
    icon={icon}
    sx={{ width, height: width, ...sx }}
    {...other}
  />
));

export type IconifyPropTypes = {
  icon:any,
  sx?: any,
  width?: number,
};

export default Iconify;
