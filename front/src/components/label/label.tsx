import React, { forwardRef } from 'react';

import Box from '@mui/material/Box';
import { useTheme } from '@mui/material/styles';

import { StyledLabel } from './styles';

// ----------------------------------------------------------------------

const Label = forwardRef(
  ({ children, color = 'default', variant = 'soft', startIcon, endIcon, sx, ...other } : LabelPropTypes, ref) => {
    const theme = useTheme();

    const iconStyles = {
      width: 16,
      height: 16,
      '& svg, img': { width: 1, height: 1, objectFit: 'cover' },
    };

    return (
      <StyledLabel
        ref={ref}
        component="span"
        ownerState={{ color, variant }}
        sx={{
          ...(startIcon && { pl: 0.75 }),
          ...(endIcon && { pr: 0.75 }),
          ...sx,
        }}
        theme={theme}
        {...other}
      >
        {startIcon && <Box sx={{ mr: 0.75, ...iconStyles }}> {startIcon} </Box>}

        {children}

        {endIcon && <Box sx={{ ml: 0.75, ...iconStyles }}> {endIcon} </Box>}
      </StyledLabel>
    );
  }
);

export type LabelPropTypes = {
  children: any,
  endIcon: any,
  startIcon: any,
  sx: any,
  variant: 'filled'| 'outlined'| 'ghost'| 'soft',
  color: 
    'default'|
    'primary'|
    'secondary'|
    'info'|
    'success'|
    'warning'|
    'error'
  ,
};

export default Label;
