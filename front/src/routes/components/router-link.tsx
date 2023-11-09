import { Link } from 'react-router-dom';
import React, { Ref , forwardRef } from 'react';

// ----------------------------------------------------------------------

const RouterLink = forwardRef(({ href, ...other } : RouterLinkPropTypes, ref: Ref<HTMLAnchorElement> | undefined) => <Link ref={ref} to={href} {...other} />);

export type RouterLinkPropTypes = {
  href: string,
};

export default RouterLink;
